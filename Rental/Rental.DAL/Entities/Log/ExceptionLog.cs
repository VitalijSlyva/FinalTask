using System;

namespace Rental.DAL.Entities.Log
{
    /// <summary>
    /// Entity for exception logs.
    /// </summary>
    public class ExceptionLog
    {
        public int Id { get; set; }

        public string ExeptionMessage { get; set; }

        public string ClassName { get; set; }

        public string ActionName { get; set; }

        public string StackTrace { get; set; }

        public DateTime Time { get; set; }
    }
}
