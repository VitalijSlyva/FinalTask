using Rental.BLL.DTO.Log;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Log;
using Rental.DAL.Interfaces;
using System.Collections.Generic;

namespace Rental.BLL.Services
{
    /// <summary>
    /// Service for logging.
    /// </summary>
    public class LogService : ILogService
    {
        private ILogMapperDTO _logMapperDTO;

        private ILogUnitOfWork _logUnitOfWork;

        /// <summary>
        /// Create mapper and unit for work.
        /// </summary>
        /// <param name="mapperDTO">Mapper object</param>
        /// <param name="logUnit">Unit of work object</param>
        public LogService(ILogMapperDTO mapperDTO,  ILogUnitOfWork logUnit)
        {
            _logUnitOfWork = logUnit;
            _logMapperDTO = mapperDTO;
        }

        /// <summary>
        /// Save action log.
        /// </summary>
        /// <param name="log">Action log object</param>
        public void CreateActionLog(ActionLogDTO log)
        {
            try
            {
                var newLog = _logMapperDTO.ToActionLog.Map<ActionLogDTO, ActionLog>(log);
                _logUnitOfWork.ActionLogger.Create(newLog);
                _logUnitOfWork.Save();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Save exception log.
        /// </summary>
        /// <param name="log">Exception log object</param>
        public void CreateExeptionLog(ExceptionLogDTO log)
        {
            try
            {
                var newLog = _logMapperDTO.ToExceptionLog.Map<ExceptionLogDTO, ExceptionLog>(log);
                _logUnitOfWork.ExceptionLogger.Create(newLog);
                _logUnitOfWork.Save();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Show all action logs.
        /// </summary>
        /// <returns>Action logs</returns>
        public IEnumerable<ActionLogDTO> ShowActionLogs()
        {
            var logs = _logUnitOfWork.ActionReporter.Show();

            return _logMapperDTO.ToActionLogDTO.Map<IEnumerable<ActionLog>, List<ActionLogDTO>>(logs);
        }

        /// <summary>
        /// Show all exception logs.
        /// </summary>
        /// <returns>Exception logs</returns>
        public IEnumerable<ExceptionLogDTO> ShowExceptionLogs()
        {
            var logs = _logUnitOfWork.ExceptionReporter.Show();

            return _logMapperDTO.ToExceptionLogDTO.Map<IEnumerable<ExceptionLog>, List<ExceptionLogDTO>>(logs);
        }
    }
}
