using Rental.DAL.EF.Contexts;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Logging
{
    public class Reporter<T> : IDisplayer<T> where T : class
    {
        private LogContext _context;

        public Reporter(LogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns IEnumerable elements for predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        /// <summary>
        /// Take element for id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// Get all elements from table.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Show()
        {
            return _context.Set<T>();
        }
    }
}
