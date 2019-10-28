using Rental.DAL.Entities.Log;
using System;

namespace Rental.DAL.Interfaces
{
    /// <summary>
    /// Interface for working with log.
    /// </summary>
    public interface ILogUnitOfWork
    {
        ICreator<ExceptionLog> ExceptionLogger { get; }

        ICreator<ActionLog> ActionLogger { get; }

        IDisplayer<ActionLog> ActionReporter { get; }

        IDisplayer<ExceptionLog> ExceptionReporter { get; }

        /// <summary>
        /// Save changes.
        /// </summary>
        void Save();
    }
}
