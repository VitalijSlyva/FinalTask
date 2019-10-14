using Rental.DAL.EF.Contexts;
using Rental.DAL.Entities.Log;
using Rental.DAL.Interfaces;
using Rental.DAL.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Services
{
    public class LogUnitOfWork : ILogUnitOfWork
    {
        private LogContext _context;

        private ICreator<ExceptionLog> _exceptionLogger;

        private ICreator<ActionLog> _actionLogger;

        private IDisplayer<ActionLog> _actionReporter;

        private IDisplayer<ExceptionLog> _exceptionReporter;

        public LogUnitOfWork(string connectionString)
        {
            _context = new LogContext(connectionString);
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

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _Disposed = false;

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
