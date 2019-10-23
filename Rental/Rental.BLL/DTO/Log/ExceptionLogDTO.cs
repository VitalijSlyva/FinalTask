using System;

namespace Rental.BLL.DTO.Log
{
    /// <summary>
    /// Data transfer object for exception log.
    /// </summary>
    public class ExceptionLogDTO
    {
        public int Id { get; set; }

        public string ExeptionMessage { get; set; }

        public string ClassName { get; set; }

        public string ActionName { get; set; }

        public string StackTrace { get; set; }

        public DateTime Time { get; set; }
    }
}
