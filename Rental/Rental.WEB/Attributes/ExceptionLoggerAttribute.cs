using Rental.BLL.DTO.Log;
using Rental.BLL.Interfaces;
using System;
using System.Web.Mvc;

namespace Rental.WEB.Attributes
{
    /// <summary>
    /// Exeption logger attribute
    /// </summary>
    public class ExceptionLoggerAttribute : FilterAttribute,IExceptionFilter
    {
        private ILogService _logService= (ILogService)DependencyResolver.Current.GetService(typeof(ILogService));

        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="filterContext">Exception context</param>
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
            filterContext.Result = new ViewResult()
            {
                ViewName = "CustomError"
            };
        } 
    }
}