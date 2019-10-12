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

        public ActionResult CreateProfile()
        {
            return View();
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

        public ActionResult UpdateProfile()
        {
            return View();
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
            var ordersDTO = await _clientService.GetOrdersForClientAsync(User.Identity.GetUserId());
            var ordersDM = _rentMapperDM.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(ordersDTO);
            var showVM = new ShowUserOrdersVM() { OrdersDM = ordersDM };
            return View(showVM);
        }

        public ActionResult MakeOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> MakeOrder(OrderDM orderDM)
        {
            if (ModelState.IsValid)
            {
                var orderDTO = _rentMapperDM.ToOrderDTO.Map<OrderDM, OrderDTO>(orderDM);
                orderDTO.Profile = await _clientService.ShowProfileAsync(User.Identity.GetUserId());
                await _clientService.MakeOrderAsync(orderDTO);
                return RedirectToAction("Index", "Rent");
            }
            return View(orderDM);
        }

        public ActionResult MakePayment()
        {
            return View();
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

        protected override void Dispose(bool disposing)
        {
            _rentService.Dispose();
            _clientService.Dispose();
            base.Dispose(disposing);
        }

    }
}