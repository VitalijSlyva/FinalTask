using Microsoft.AspNet.Identity;
using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.WEB.Attributes;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Identity;
using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Controllers
{
    [Authorize(Roles="client")]
    [AuthorizeWithoutBann]
    public class ClientController : Controller
    {
        private IClientService _clientService;

        private IRentService _rentService;

        private IIdentityMapperDM _identityMapperDM;

        private IRentMapperDM _rentMapperDM;

        public ClientController(IClientService clientService, IIdentityMapperDM identityMapperDM, IRentMapperDM rentMapperDM, IRentService rentService)
        {
            _clientService = clientService;
            _identityMapperDM = identityMapperDM;
            _rentMapperDM = rentMapperDM;
            _rentService = rentService;
        }

        public async Task<ActionResult> CreateProfile()
        {
            if((await _clientService.ShowProfileAsync(User.Identity.GetUserId()))==null)
            return View();
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult CreateProfile(ProfileDM profileDM)
        {
            if (ModelState.IsValid)
            {
                ProfileDTO profileDTO = _identityMapperDM.ToProfileDTO.Map<ProfileDM, ProfileDTO>(profileDM);
                profileDTO.User = new User() { Id = User.Identity.GetUserId() };
                _clientService.CreateProfileAsync(profileDTO);
                return RedirectToAction("ShowProfile");
            }
            return View(profileDM);
        }

        public async Task<ActionResult> UpdateProfile()
        {
            if ((await _clientService.ShowProfileAsync(User.Identity.GetUserId())) == null)
                return new HttpNotFoundResult();
            var profile = _identityMapperDM.ToProfileDM.Map<ProfileDTO, ProfileDM>(await _clientService.ShowProfileAsync(User.Identity.GetUserId()));
            return View(profile);
        }

        [HttpPost]
        public ActionResult UpdateProfile(ProfileDM profileDM)
        {
            if (ModelState.IsValid)
            {
                ProfileDTO profileDTO = _identityMapperDM.ToProfileDTO.Map<ProfileDM, ProfileDTO>(profileDM);
                profileDTO.User = new User() { Id = User.Identity.GetUserId() };
                _clientService.UpdateProfileAsync(profileDTO);
                return RedirectToAction("ShowProfile");
            }
            return View(profileDM);
        }

        public async Task<ActionResult> ShowProfile()
        {
            var profile = await _clientService.ShowProfileAsync(User.Identity.GetUserId());
            if (profile == null)
                return new HttpNotFoundResult();
            var profileDM = _identityMapperDM.ToProfileDM.Map<ProfileDTO, ProfileDM>(profile);
            return View(profileDM);
        }

        public async Task<ActionResult> ShowUserOrders()
        {
            var ordersDTO = (await _clientService.GetOrdersForClientAsync(User.Identity.GetUserId())).OrderByDescending(x=>x.DateStart);
            var ordersDM = _rentMapperDM.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(ordersDTO);
            Dictionary<int, string> statuses = new Dictionary<int, string>();
            foreach(var i in ordersDM)
            {
                statuses.Add(i.Id, _clientService.GetStatus(i.Id));
            }
            var showVM = new ShowUserOrdersVM() { OrdersDM = ordersDM,Statuses=statuses };
            return View(showVM);
        }

        public async Task<ActionResult> MakeOrder(int? carId)
        {
            if ((await _clientService.ShowProfileAsync(User.Identity.GetUserId())) != null&&carId!=null)
            {
                var carDTO=_rentService.GetCar(carId.Value);
                if(carDTO==null)
                    return new HttpNotFoundResult();
                var car = _rentMapperDM.ToCarDM.Map<CarDTO, CarDM>(carDTO);
                return View(new OrderDM() { Car=car});
            }
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public async Task<ActionResult> MakeOrder(OrderDM orderDM)
        {
            if (ModelState.IsValid)
            {
                var orderDTO = _rentMapperDM.ToOrderDTO.Map<OrderDM, OrderDTO>(orderDM);
                orderDTO.Profile = await _clientService.ShowProfileAsync(User.Identity.GetUserId());
                await _clientService.MakeOrderAsync(orderDTO);
                var paymentId = (await _clientService.GetOrdersForClientAsync(User.Identity.GetUserId())).Last().Payment.Id;
                return RedirectToAction("MakePayment", "Client",new { id=paymentId});
            }
            var carDTO = _rentService.GetCar(orderDM.Car.Id);
            var car = _rentMapperDM.ToCarDM.Map<CarDTO, CarDM>(carDTO);
            orderDM.Car = car;
            return View(orderDM);
        }

        public ActionResult MakePayment(int?id)
        {
            if (id != null)
            {
                var paymentDTO = _clientService.GetPayment(id.Value);
                if(paymentDTO==null)
                    return new HttpNotFoundResult();
                return View(_rentMapperDM.ToPaymentDM.Map<PaymentDTO, PaymentDM>(paymentDTO));
            }
            return new HttpNotFoundResult();
        }

        [HttpPost] 
        public ActionResult MakePayment(PaymentDM paymentDM)
        {
            if (ModelState.IsValid)
            {
                _clientService.CreatePayment(paymentDM.Id, paymentDM.TransactionId);
                return RedirectToAction("Index", "Home");
            }
            return View(paymentDM);
        }

        public ActionResult ShowPayments()
        {
            var paymentsDTO = _clientService.GetPaymentsForClient(User.Identity.GetUserId()).OrderBy(x=>x.IsPaid);
            if (paymentsDTO == null)
                return View(new ShowPaymentsVM());
            var payments = _rentMapperDM.ToPaymentDM.Map<IEnumerable<PaymentDTO>, List<PaymentDM>>(paymentsDTO);
            return View(new ShowPaymentsVM() { Payments = payments });
        }

        protected override void Dispose(bool disposing)
        {
            _rentService.Dispose();
            _clientService.Dispose();
            base.Dispose(disposing);
        }

    }
}