using System;

namespace Rental.DAL.Entities.Log
{
    /// <summary>
    /// Entity for action log.
    /// </summary>
    public class ActionLog
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string Action { get; set; }

        public DateTime Time { get; set; }
    }
}
