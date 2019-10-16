using Microsoft.AspNet.Identity;
using Rental.BLL.DTO.Log;
using Rental.BLL.DTO.Rent;
using Rental.BLL.Interfaces;
using Rental.WEB.Attributes;
using Rental.WEB.Interfaces;
using Rental.WEB.Models.Domain_Models.Rent;
using Rental.WEB.Models.View_Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Controllers
{
    [ExceptionLogger]
    [Authorize(Roles = "manager")]
    [AuthorizeWithoutBann]
    public class ManagerController : Controller
    {
        private IManagerService _managerService;

        private IRentMapperDM _rentMapperDM;

        private ILogWriter _logWriter;

        public ManagerController(IManagerService managerService, IRentMapperDM rentMapper, ILogWriter log)
        {
            _managerService = managerService;
            _rentMapperDM = rentMapper;
            _logWriter = log;
        }

        public ActionResult Confirm(int? id)
        {
            if (id != null)
            {
                var orderDTO = _managerService.GetOrder(id.Value,true);
                if (orderDTO == null)
                    return new HttpNotFoundResult();
                ConfirmDM confirm = new ConfirmDM() { Order = _rentMapperDM.ToOrderDM.Map<OrderDTO, OrderDM>(orderDTO) };
                return View(confirm);
            }
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Confirm(ConfirmDM confirmDM)
        {
            if (!confirmDM.IsConfirmed&&String.IsNullOrEmpty(confirmDM.Description))
            {
                ModelState.AddModelError("", "Не указана причина отклонения");
            }
            if (ModelState.IsValid)
            {
                ConfirmDTO confirm = _rentMapperDM.ToConfirmDTO.Map<ConfirmDM, ConfirmDTO>(confirmDM);
                confirm.User = new BLL.DTO.Identity.User() { Id = User.Identity.GetUserId() };
                _managerService.ConfirmOrder(confirm);
                _logWriter.CreateLog("Подтвердил заказ"+confirm.Order.Id, User.Identity.GetUserId());
                return RedirectToAction("ShowConfirms", "Manager", null);
            }
            var orderDTO = _managerService.GetOrder(confirmDM.Order.Id,true);
            confirmDM.Order = _rentMapperDM.ToOrderDM.Map<OrderDTO, OrderDM>(orderDTO);
            return View(confirmDM);
        }

        public ActionResult ShowConfirms()
        {
            var orders = _managerService.GetForConfirms();
            var ordersDM = _rentMapperDM.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(orders);
            ShowConfirmsVM confirmsVM = new ShowConfirmsVM() { Orders=ordersDM };
            return View(confirmsVM);
        }

        public ActionResult ShowReturns()
        {
            var orders = _managerService.GetForReturns();
            var ordersDM = _rentMapperDM.ToOrderDM.Map<IEnumerable<OrderDTO>, List<OrderDM>>(orders);
            ShowReturnsVM returnsVM = new ShowReturnsVM() { Orders = ordersDM };
            return View(returnsVM);
        }

        public ActionResult Return(int? id)
        {
            if (id != null)
            {
                var orderDTO = _managerService.GetOrder(id.Value, false);
                if (orderDTO == null)
                    return new HttpNotFoundResult();
                ReturnDM returnDM = new ReturnDM() { Order = _rentMapperDM.ToOrderDM.Map<OrderDTO, OrderDM>(orderDTO) };
                return View(returnDM);
            }
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Return(ReturnDM returnDM,bool withCrash=false)
        {

        //    if (ModelState.IsValid||!withCrash)
     //       {
                ReturnDTO returnDTO = _rentMapperDM.ToReturnDTO.Map<ReturnDM, ReturnDTO>(returnDM);
                returnDTO.User = new BLL.DTO.Identity.User() { Id = User.Identity.GetUserId() };
                if (withCrash == false)
                returnDTO.Crash =null;
                _managerService.ReturnCar(returnDTO);
                _logWriter.CreateLog("Отклонил заказ" + returnDTO.Order.Id, User.Identity.GetUserId());
                return RedirectToAction("ShowReturns", "Manager", null);
       //     }
            //var orderDTO = _managerService.GetOrder(returnDM.Order.Id, false);
            //returnDM = new ReturnDM() { Order = _rentMapperDM.ToOrderDM.Map<OrderDTO, OrderDM>(orderDTO) };
            //return View(returnDM);
        }

        protected override void Dispose(bool disposing)
        {
            _managerService.Dispose();
            base.Dispose(disposing);
        }
    }
}