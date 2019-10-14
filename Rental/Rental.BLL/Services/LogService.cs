using Rental.BLL.Abstracts;
using Rental.BLL.DTO.Log;
using Rental.BLL.Interfaces;
using Rental.DAL.Entities.Log;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Services
{
    public class LogService : ILogService
    {
        private ILogMapperDTO _logMapperDTO;

        private ILogUnitOfWork _logUnitOfWork;

        public LogService(ILogMapperDTO mapperDTO,  ILogUnitOfWork logUnit)
        {
            _logUnitOfWork = logUnit;
            _logMapperDTO = mapperDTO;
        }

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

        public void Dispose()
        {
            _logUnitOfWork.Dispose();
        }

        public IEnumerable<ActionLogDTO> ShowActionLogs()
        {
            var logs = _logUnitOfWork.ActionReporter.Show();
            return _logMapperDTO.ToActionLogDTO.Map<IEnumerable<ActionLog>, List<ActionLogDTO>>(logs);
        }

        public IEnumerable<ExceptionLogDTO> ShowExceptionLogs()
        {
            var logs = _logUnitOfWork.ExceptionReporter.Show();
            return _logMapperDTO.ToExceptionLogDTO.Map<IEnumerable<ExceptionLog>, List<ExceptionLogDTO>>(logs);
        }
    }
}
