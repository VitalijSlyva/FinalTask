using Rental.BLL.DTO.Log;
using Rental.BLL.Interfaces;
using Rental.WEB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Infrastructure
{
    public class LogWriter : ILogWriter
    {
        private ILogService _logService;

        public LogWriter(ILogService logService)
        {
            _logService = logService;
        }

        public void CreateLog(string action, string authorId)
        {
            ActionLogDTO log = new ActionLogDTO()
            {
                Action = action,
                Time = DateTime.Now,
                AuthorId = authorId
            };
            _logService.CreateActionLog(log);
        }
    }
}