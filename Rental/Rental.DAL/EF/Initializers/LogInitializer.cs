using Rental.DAL.EF.Contexts;
using System.Data.Entity;

namespace Rental.DAL.EF.Initializers
{
    /// <summary>
    /// Initializator for log context.
    /// </summary>
    internal class LogInitializer: CreateDatabaseIfNotExists<LogContext>
    //internal class LogInitializer: DropCreateDatabaseAlways<LogContext>
    {
        /// <summary>
        /// Recreate database with new items.
        /// </summary>
        /// <param name="context">Database context</param>
        protected override void Seed(LogContext context)
        {
            base.Seed(context);
        }
    }
}
