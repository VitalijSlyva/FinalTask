using Rental.DAL.EF.Contexts;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rental.DAL.Logging
{
    /// <summary>
    /// Reporter for logs.
    /// </summary>
    /// <typeparam name="T">Log entity</typeparam>
    public class Reporter<T> : IDisplayer<T> where T : class
    {
        private LogContext _context;

        /// <summary>
        /// Create context for work.
        /// </summary>
        /// <param name="context">Log context</param>
        public Reporter(LogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns logs for predicate.
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns>Logs</returns>
        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        /// <summary>
        /// Take log by id.
        /// </summary>
        /// <param name="id">Log id</param>
        /// <returns>Log</returns>
        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// Get all logs from table.
        /// </summary>
        /// <returns>Logs</returns>
        public IEnumerable<T> Show()
        {
            return _context.Set<T>();
        }
    }
}
