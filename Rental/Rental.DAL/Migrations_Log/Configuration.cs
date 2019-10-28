namespace Rental.DAL.Migrations_Log
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Rental.DAL.EF.Contexts.LogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations_Log";
            ContextKey = "Rental.DAL.EF.Contexts.LogContext";
        }

        protected override void Seed(Rental.DAL.EF.Contexts.LogContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
