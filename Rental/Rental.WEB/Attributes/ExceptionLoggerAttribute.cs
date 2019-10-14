using Ninject;
using Rental.BLL.DTO.Log;
using Rental.BLL.Interfaces;
using Rental.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rental.WEB.Attributes
{
    public class ExceptionLoggerAttribute : FilterAttribute,IExceptionFilter
    {
        [Inject]
        private ILogService _logService;

        public void OnException(ExceptionContext filterContext)
        {
            ExceptionLogDTO exceptionLogDTO = new ExceptionLogDTO()
            {
                ExeptionMessage = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                ClassName=filterContext.RouteData.Values["controller"].ToString(),
                ActionName=filterContext.RouteData.Values["action"].ToString(),
                Time=DateTime.Now
            };
            _logService.CreateExeptionLog(exceptionLogDTO);
            filterContext.ExceptionHandled = true;
        } 
    }
}