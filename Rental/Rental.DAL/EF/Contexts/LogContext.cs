using Rental.DAL.EF.Initializers;
using Rental.DAL.Entities.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.EF.Contexts
{
    public class LogContext : DbContext
    {
        static LogContext()
        {
            Database.SetInitializer<LogContext>(new LogInitializer());
        }

        public LogContext() { }

        public LogContext(string connection) : base(connection)
        {

        }

        public DbSet<ActionLog> ActionLogs { get; set; }

        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    }
}
