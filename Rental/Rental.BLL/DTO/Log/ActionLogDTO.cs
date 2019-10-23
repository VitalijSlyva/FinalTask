using System;

namespace Rental.BLL.DTO.Log
{
    /// <summary>
    /// Data transfer object for action log.
    /// </summary>
    public class ActionLogDTO
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string Action { get; set; }

        public DateTime Time { get; set; }
    }
}
