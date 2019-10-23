using Rental.DAL.EF.Contexts;
using Rental.DAL.Interfaces;

namespace Rental.DAL.Logging
{
    /// <summary>
    /// Log creator.
    /// </summary>
    /// <typeparam name="T">Log entity</typeparam>
    public class Logger<T> : ICreator<T> where T: class
    {
        private LogContext _logContext;

        /// <summary>
        /// Save context for work.
        /// </summary>
        /// <param name="context">Log context</param>
        public Logger(LogContext context)
        {
            _logContext = context;
        }

        /// <summary>
        /// Create new log.
        /// </summary>
        /// <param name="item">New log.</param>
        public void Create(T item)
        {
            _logContext.Entry(item).State = System.Data.Entity.EntityState.Added;
        }
    }
}
