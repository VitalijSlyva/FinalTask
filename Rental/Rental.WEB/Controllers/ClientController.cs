﻿using Microsoft.AspNet.Identity;
using Rental.BLL.DTO.Identity;
using Rental.BLL.DTO.Log;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.WEB.Attributes;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Identity;
using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Client;
using Rental.WEB.Models.View_Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Controllers
{
    [ExceptionLogger]
    [Authorize(Roles="client")]
    [AuthorizeWithoutBann]
    public class ClientController : Controller
    {
        private IClientService _clientService;

        private IRentService _rentService;

        private IIdentityMapperDM _identityMapperDM;

        private IRentMapperDM _rentMapperDM;

        private ILogWriter _logWriter;

        public ClientController(IClientService clientService, IIdentityMapperDM identityMapperDM,
            IRentMapperDM rentMapperDM, IRentService rentService, ILogWriter log)
        {
            _clientService = clientService;
            _identityMapperDM = identityMapperDM;
            _rentMapperDM = rentMapperDM;
            _rentService = rentService;
            _logWriter = log;
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
            if (profileDM.DateOfBirth.Date > DateTime.Now.AddYears(-18).Date
                  || profileDM.DateOfBirth.Date < DateTime.Now.AddYears(-80).Date)
                ModelState.AddModelError("DateOfBirth", "Неверная дата");
            if (profileDM.DateOfExpiry.Date < DateTime.Now.Date)
                ModelState.AddModelError("DateOfExpiry", "Неверная дата");
            if (profileDM.DateOfIssue.Date > DateTime.Now.Date ||
                profileDM.DateOfIssue.Date < DateTime.Now.AddYears(-50).Date)
                ModelState.AddModelError("DateOfIssue", "Неверная дата");
            if (ModelState.IsValid)
            {
                ProfileDTO profileDTO = _identityMapperDM.ToProfileDTO.Map<ProfileDM, ProfileDTO>(profileDM);
                profileDTO.User = new User() { Id = User.Identity.GetUserId() };
                _clientService.CreateProfileAsync(profileDTO);
                _logWriter.CreateLog("Добавил паспортные данные", User.Identity.GetUserId());
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
            if (profileDM.DateOfBirth.Date > DateTime.Now.AddYears(-18).Date
                || profileDM.DateOfBirth.Date<DateTime.Now.AddYears(-80).Date)
                ModelState.AddModelError("DateOfBirth", "Неверная дата");
            if (profileDM.DateOfExpiry.Date < DateTime.Now.Date)
                ModelState.AddModelError("DateOfExpiry", "Неверная дата");
            if (profileDM.DateOfIssue.Date > DateTime.Now.Date||
                profileDM.DateOfIssue.Date < DateTime.Now.AddYears(-50).Date)
                ModelState.AddModelError("DateOfIssue", "Неверная дата");
            if (ModelState.IsValid)
            {
                ProfileDTO profileDTO = _identityMapperDM.ToProfileDTO.Map<ProfileDM, ProfileDTO>(profileDM);
                profileDTO.User = new User() { Id = User.Identity.GetUserId() };
                _clientService.UpdateProfile(profileDTO);
                _logWriter.CreateLog("Обновил паспортные данные", User.Identity.GetUserId());
                return RedirectToAction("ShowProfile");
            }
            return View(profileDM);
        }

        public async Task<ActionResult> ShowProfile()
        {
            var profile = await _clientService.ShowProfileAsync(User.Identity.GetUserId());
            if (profile == null)
                return RedirectToAction("CreateProfile");
            var profileDM = _identityMapperDM.ToProfileDM.Map<ProfileDTO, ProfileDM>(profile);
            return View(profileDM);
        }

        public async Task<ActionResult> ShowUserOrders(ShowUserOrdersVM model, int sortMode = 0, int page = 1, int selectedMode = 1)
        {
            if (sortMode == 0)
            {
                sortMode = selectedMode;
            }
            var ordersDTO = (await _clientService.GetOrdersForClientAsync(User.Identity.GetUserId())).OrderByDescending(x=>x.DateStart);
            var ordersDM = _rentMapperDM.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(ordersDTO);
            Dictionary<int, string> statuses = new Dictionary<int, string>();
            foreach(var i in ordersDM)
            {
                statuses.Add(i.Id, _clientService.GetStatus(i.Id));
            }
            var filters = new List<Models.View_Models.Shared.Filter>();
            if (ordersDM != null && ordersDM.Count > 0)
            {
                if (model.Filters == null || model.Filters.Count == 0)
                {
                    void CreateFilters(string name, Func<OrderDM, string> value)
                    {
                        filters.AddRange(ordersDM.Select(x => value(x)).Distinct()
                            .Select(x => new Models.View_Models.Shared.Filter() { Name = name, Text = x, Checked = false }));
                    }
                    CreateFilters("Автомобиль", x => x.Car.Brand.Name + " " + x.Car.Model);
                    CreateFilters("Оплата", x => x.Payment.IsPaid ? "Оплачен" : "Неоплачен");
                    CreateFilters("Статус",x=>statuses[x.Id].Split(' ')[0]=="Отклонен"? statuses[x.Id].Split(' ')[0]:statuses[x.Id]);

                }
                else
                {
                    filters = model.Filters;
                    void FilterTest(string name, Func<OrderDM, string> value)
                    {
                        if (ordersDM.Count > 0 && model.Filters.Any(f => f.Name == name && f.Checked))
                        {
                            ordersDM = ordersDM.Where(p => model.Filters.Any(f => f.Name == name && f.Text == value(p) && f.Checked)).ToList();
                        }
                    }

                    FilterTest("Автомобиль", x => x.Car.Brand.Name + " " + x.Car.Model);
                    FilterTest("Оплата", x => x.Payment.IsPaid ? "Оплачен" : "Неоплачен");
                    FilterTest("Статус", x => statuses[x.Id].Split(' ')[0] == "Отклонен" ? statuses[x.Id].Split(' ')[0] : statuses[x.Id]);
                }
            }

            var sortModes = new List<string>();
            sortModes.Add("По статусу");
            sortModes.Add("По статусу");
            sortModes.Add("По цене");
            sortModes.Add("По цене");
            sortModes.Add("По автомобилю");
            sortModes.Add("По автомобилю");
            switch (sortMode)
            {
                case 1:
                    ordersDM = ordersDM.OrderBy(x => statuses[x.Id]).ToList();
                    break;
                case 2:
                    ordersDM = ordersDM.OrderByDescending(x => statuses[x.Id]).ToList();
                    break;
                case 3:
                    ordersDM = ordersDM.OrderBy(x => x.Payment.Price).ToList();
                    break;
                case 4:
                    ordersDM = ordersDM.OrderByDescending(x => x.Payment.Price).ToList();
                    break;
                case 5:
                    ordersDM = ordersDM.OrderBy(x => x.Car.Brand.Name+" "+x.Car.Model).ToList();
                    break;
                case 6:
                    ordersDM = ordersDM.OrderByDescending(x => x.Car.Brand.Name + " " + x.Car.Model).ToList();
                    break;
            }

            int pageSize = 2;
            int count = ordersDM.Count;
            ordersDM = ordersDM.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItem = count };

            var showVM = new ShowUserOrdersVM() {
                OrdersDM = ordersDM,
                Statuses =statuses,
                Filters =filters,
                PageInfo =pageInfo,
                SortModes = sortModes,
                SelectedMode = sortMode
            };
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
                return View(new OrderDM() {
                    Car =car,
                    DateStart =DateTime.Now,
                    DateEnd =DateTime.Now
                });
            }
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public async Task<ActionResult> MakeOrder(OrderDM orderDM)
        {
            if (orderDM.DateStart.Date < DateTime.Now.Date || orderDM.DateEnd.Date < orderDM.DateStart.Date)
            {
                ModelState.AddModelError("", "Проверте корректоность введенных дат");
            }
            else
            {
                var orderDTO = _rentMapperDM.ToOrderDTO.Map<OrderDM, OrderDTO>(orderDM);
                orderDTO.Profile = await _clientService.ShowProfileAsync(User.Identity.GetUserId());
                await _clientService.MakeOrderAsync(orderDTO);
                var paymentId = (await _clientService.GetOrdersForClientAsync(User.Identity.GetUserId())).Last().Payment.Id;
                _logWriter.CreateLog("Заказал авто", User.Identity.GetUserId());
                return RedirectToAction("MakePayment", "Client", new { id = paymentId });
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
                _logWriter.CreateLog("Произвел оплату" +paymentDM.Id, User.Identity.GetUserId());
                return RedirectToAction("Index", "Rent");
            }
            return View(paymentDM);
        }

        public ActionResult ShowPayments(ShowPaymentsVM model, int sortMode = 0, int page = 1, int selectedMode = 1)
        {
            if (sortMode == 0)
            {
                sortMode = selectedMode;
            }
            var paymentsDTO = _clientService.GetPaymentsForClient(User.Identity.GetUserId()).OrderBy(x=>x.IsPaid);
            if (paymentsDTO == null)
                return View(new ShowPaymentsVM());
            var payments = _rentMapperDM.ToPaymentDM.Map<IEnumerable<PaymentDTO>, List<PaymentDM>>(paymentsDTO);
            var filters = new List<Models.View_Models.Shared.Filter>();
            int? minPrice = 0,
                 maxPrice = 0,
                 minCurPrice = 0,
                 maxCurPrice = 0;
            if (payments != null && payments.Count > 0)
            {
                if (model.Filters == null || model.Filters.Count == 0)
                {
                    void CreateFilters(string name, Func<PaymentDM, string> value)
                    {
                        filters.AddRange(payments.Select(x => value(x)).Distinct()
                            .Select(x => new Models.View_Models.Shared.Filter() { Name = name, Text = x, Checked = false }));
                    }
                    CreateFilters("Статус", x => x.IsPaid?"Оплачен":"Неоплачен");
                    maxCurPrice = maxPrice = payments.Max(x => x.Price) + 1;
                    minCurPrice = minPrice = payments.Min(x => x.Price) - 1;
                }
                else
                {
                    filters = model.Filters;
                    minCurPrice = model.CurrentPriceMin;
                    maxCurPrice = model.CurrentPriceMax;
                    minPrice = model.PriceMin;
                    maxPrice = model.PriceMax;

                    void FilterTest(string name, Func<PaymentDM, string> value)
                    {
                        if (payments.Count > 0 && model.Filters.Any(f => f.Name == name && f.Checked))
                        {
                            payments = payments.Where(p => model.Filters.Any(f => f.Name == name && f.Text == value(p) && f.Checked)).ToList();
                        }
                    }

                    FilterTest("Статус", x => x.IsPaid ? "Оплачен" : "Неоплачен");
                    payments = payments.Where(p => p.Price >= model.CurrentPriceMin && p.Price <= model.CurrentPriceMax).ToList();
                }
            }

            var sortModes = new List<string>();
            sortModes.Add("По статусу");
            sortModes.Add("По статусу");
            sortModes.Add("По сумме");
            sortModes.Add("По сумме");
            switch (sortMode)
            {
                case 1:
                    payments = payments.OrderBy(x => x.IsPaid).ToList();
                    break;
                case 2:
                    payments = payments.OrderByDescending(x => x.IsPaid).ToList();
                    break;
                case 3:
                    payments = payments.OrderBy(x => x.Price).ToList();
                    break;
                case 4:
                    payments = payments.OrderByDescending(x => x.Price).ToList();
                    break;
            }

            int pageSize = 2;
            int count = payments.Count;
            payments = payments.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItem = count };

            var paymenthsVM = new ShowPaymentsVM()
            {
                Payments = payments,
                Filters=filters,
                CurrentPriceMax=maxCurPrice,
                CurrentPriceMin=minCurPrice,
                PriceMax=maxPrice,
                PriceMin=minPrice,
                PageInfo=pageInfo,
                SortModes = sortModes,
                SelectedMode = sortMode
            };
            return View(paymenthsVM);
        }

        protected override void Dispose(bool disposing)
        {
            _rentService.Dispose();
            _clientService.Dispose();
            base.Dispose(disposing);
        }

    }
}