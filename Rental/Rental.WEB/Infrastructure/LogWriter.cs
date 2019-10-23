using Rental.BLL.DTO.Log;
using Rental.BLL.Interfaces;
using Rental.WEB.Interfaces;
using System;

namespace Rental.WEB.Infrastructure
{
    /// <summary>
    /// Write logs in service.
    /// </summary>
    public class LogWriter : ILogWriter
    {
        private ILogService _logService;

        /// <summary>
        /// Create connection with service
        /// </summary>
        /// <param name="logService">Log service</param>
        public LogWriter(ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// Create new action log.
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="authorId">Author</param>
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