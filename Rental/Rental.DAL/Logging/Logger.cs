using Rental.DAL.EF.Contexts;
using Rental.DAL.Entities.Log;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Logging
{
    public class Logger<T> : ICreator<T> where T: class
    {
        private LogContext _logContext;

        public Logger(LogContext context)
        {
            _logContext = context;
        }

        public void Create(T item)
        {
            _logContext.Entry(item).State = System.Data.Entity.EntityState.Added;
        }
    }
}
