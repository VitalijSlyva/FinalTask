using Rental.BLL.DTO.Log;
using System;
using System.Collections.Generic;

namespace Rental.BLL.Interfaces
{
    /// <summary>
    /// Interface for logging actions.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Save exception log.
        /// </summary>
        /// <param name="log">Exception log object</param>
        void CreateExeptionLog(ExceptionLogDTO log);

        /// <summary>
        /// Save action log.
        /// </summary>
        /// <param name="log">Action log object</param>
        void CreateActionLog(ActionLogDTO log);

        /// <summary>
        /// Show all action logs.
        /// </summary>
        /// <returns>Action logs</returns>
        IEnumerable<ActionLogDTO> ShowActionLogs();

        /// <summary>
        /// Show all exception logs.
        /// </summary>
        /// <returns>Exception logs</returns>
        IEnumerable<ExceptionLogDTO> ShowExceptionLogs(); 
    }
}
