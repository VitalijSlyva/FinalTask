using Rental.DAL.Entities.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Interfaces
{
    public interface ILogUnitOfWork :IDisposable
    {
        ICreator<ExceptionLog> ExceptionLogger { get; }

        ICreator<ActionLog> ActionLogger { get; }

        IDisplayer<ActionLog> ActionReporter { get; }

        IDisplayer<ExceptionLog> ExceptionReporter { get; }

        void Save();
    }
}
