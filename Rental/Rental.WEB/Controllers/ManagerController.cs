using Microsoft.AspNet.Identity;
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
    [Authorize(Roles = "manager")]
    [AuthorizeWithoutBann]
    public class ManagerController : Controller
    {
        private IManagerService _managerService;

        private IRentMapperDM _rentMapperDM;

        public ManagerController(IManagerService managerService, IRentMapperDM rentMapper)
        {
            _managerService = managerService;
            _rentMapperDM = rentMapper;
        }
        // GET: Manager
        public ActionResult Confirm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Confirm(ConfirmDM confirmDM)
        {
            if (ModelState.IsValid)
            {
                ConfirmDTO confirm = _rentMapperDM.ToConfirmDTO.Map<ConfirmDM, ConfirmDTO>(confirmDM);
                confirm.User.Id = User.Identity.GetUserId();
                _managerService.ConfirmOrder(confirm);
                return RedirectToAction("ShowConfirms", "Manager", null);
            }
            return View(confirmDM);
        }

        public ActionResult ShowConfirms()
        {
            var confirmsDTO = _managerService.GetConfirms();
            List<ConfirmDM> confirms = _rentMapperDM.ToConfirmDM.Map<IEnumerable<ConfirmDTO>, List<ConfirmDM>>(confirmsDTO);
            ShowConfirmsVM confirmsVM = new ShowConfirmsVM() { Confirms = confirms };
            return View(confirmsVM);
        }

        public ActionResult ShowReturns()
        {
            var returnsDTO = _managerService.GetReturns();
            List<ReturnDM> returns = _rentMapperDM.ToReturnDM.Map<IEnumerable<ReturnDTO>, List<ReturnDM>>(returnsDTO);
            ShowReturnsVM returnsVM = new ShowReturnsVM() { ReturnsDM = returns };
            return View(returnsVM);
        }

        public ActionResult ShowConfirm(int? id)
        {
            if (id!=null)
            {
                var confirmDTO = _managerService.GetConfirm(id.Value);
                if(confirmDTO==null)
                    return new HttpNotFoundResult();
                ConfirmDM confirm = _rentMapperDM.ToConfirmDM.Map<ConfirmDTO, ConfirmDM>(confirmDTO);
                return View(confirm);
            }
            return new HttpNotFoundResult();
        }

        public ActionResult ShowReturn(int? id)
        {
            if (id!=null)
            {
                var returnDTO = _managerService.GetReturn(id.Value);
                if (returnDTO == null)
                    return new HttpNotFoundResult();
                ReturnDM returnDM = _rentMapperDM.ToReturnDM.Map<ReturnDTO, ReturnDM>(returnDTO);
                return View(returnDM);
            }
            return new HttpNotFoundResult();
        }

        public ActionResult Return()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result(ReturnDM returnDM)
        {
            if (ModelState.IsValid)
            {
                ReturnDTO returnDTO = _rentMapperDM.ToReturnDTO.Map<ReturnDM, ReturnDTO>(returnDM);
                returnDM.User.Id = User.Identity.GetUserId();
                _managerService.ReturnCar(returnDTO);
                return RedirectToAction("ShowReturns", "Manager", null);
            }
            return View(returnDM);
        }

        protected override void Dispose(bool disposing)
        {
            _managerService.Dispose();
            base.Dispose(disposing);
        }
    }
}