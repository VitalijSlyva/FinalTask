namespace Rental.DAL.Entities.Rent
{
    /// <summary>
    /// Confirm entity.
    /// </summary>
    public class Confirm:Entity
    {
        public string ManagerId { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }

        public bool IsConfirmed { get; set; }

        public string Description { get; set; }
    }
}
