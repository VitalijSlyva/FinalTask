using Rental.DAL.EF.Initializers;
using Rental.DAL.Entities.Log;
using System.Data.Entity;

namespace Rental.DAL.EF.Contexts
{
    /// <summary>
    /// Context for log entities.
    /// </summary>
    public class LogContext : DbContext
    {
        /// <summary>
        /// Create initializator for database.
        /// </summary>
        static LogContext()
        {
            Database.SetInitializer<LogContext>(new LogInitializer());
        }

        /// <summary>
        /// Default builder.
        /// </summary>
        public LogContext() { }

        /// <summary>
        /// Connect to database.
        /// </summary>
        /// <param name="connection">Connection string</param>
        public LogContext(string connection) : base(connection)
        {

        }

        /// <summary>
        /// Table for action log.
        /// </summary>
        public DbSet<ActionLog> ActionLogs { get; set; }

        /// <summary>
        /// Table for exception log.
        /// </summary>
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    }
}
