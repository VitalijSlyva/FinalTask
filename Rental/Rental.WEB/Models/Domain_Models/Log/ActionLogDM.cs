using System;

namespace Rental.WEB.Models.Domain_Models.Log
{
    /// <summary>
    /// Action log model.
    /// </summary>
    public class ActionLogDM
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string Action { get; set; }

        public DateTime Time { get; set; }
    }
}