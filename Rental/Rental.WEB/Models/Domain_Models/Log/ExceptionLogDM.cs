using System;

namespace Rental.WEB.Models.Domain_Models.Log
{
    /// <summary>
    /// Exception log model.
    /// </summary>
    public class ExceptionLogDM
    {
        public int Id { get; set; }

        public string ExeptionMessage { get; set; }

        public string ClassName { get; set; }

        public string ActionName { get; set; }

        public string StackTrace { get; set; }

        public DateTime Time { get; set; }
    }
}