using Rental.BLL.DTO.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.Interfaces
{
    public interface ILogService:IDisposable
    {
        void CreateExeptionLog(ExceptionLogDTO log);

        void CreateActionLog(ActionLogDTO log);

        IEnumerable<ActionLogDTO> ShowActionLogs();

        IEnumerable<ExceptionLogDTO> ShowExceptionLogs(); 
    }
}
