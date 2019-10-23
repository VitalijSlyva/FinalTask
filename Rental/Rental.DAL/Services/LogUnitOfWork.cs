using Rental.DAL.EF.Contexts;
using Rental.DAL.Entities.Log;
using Rental.DAL.Interfaces;
using Rental.DAL.Logging;
using System;

namespace Rental.DAL.Services
{
    /// <summary>
    /// Unit for work with log.
    /// </summary>
    public class LogUnitOfWork : ILogUnitOfWork
    {
        private LogContext _context;

        private ICreator<ExceptionLog> _exceptionLogger;

        private ICreator<ActionLog> _actionLogger;

        private IDisplayer<ActionLog> _actionReporter;

        private IDisplayer<ExceptionLog> _exceptionReporter;

        /// <summary>
        /// Create log context with connection.
        /// </summary>
        /// <param name="connection">Connection string</param>
        public LogUnitOfWork(string connection)
        {
            _context = new LogContext(connection);
        }

        public ICreator<ExceptionLog> ExceptionLogger
        {
            get
            {
                if (_exceptionLogger == null)
                    _exceptionLogger = new Logger<ExceptionLog>(_context);
                return _exceptionLogger;
            }
        }

        public ICreator<ActionLog> ActionLogger
        {
            get
            {
                if (_actionLogger == null)
                    _actionLogger = new Logger<ActionLog>(_context);
                return _actionLogger;
            }
        }

        public IDisplayer<ActionLog> ActionReporter
        {
            get
            {
                if (_actionReporter == null)
                    _actionReporter = new Reporter<ActionLog>(_context);
                return _actionReporter;
            }
        }

        public IDisplayer<ExceptionLog> ExceptionReporter
        {
            get
            {
                if (_exceptionReporter == null)
                    _exceptionReporter = new Reporter<ExceptionLog>(_context);
                return _exceptionReporter;
            }
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Dispose context.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _Disposed = false;

        /// <summary>
        /// Call method for dispose.
        /// </summary>
        /// <param name="disposing">Need disposing</param>
        public virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _Disposed = true;
        }
    }
}
