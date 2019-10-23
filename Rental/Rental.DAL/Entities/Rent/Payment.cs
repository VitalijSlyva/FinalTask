namespace Rental.DAL.Entities.Rent
{
    /// <summary>
    /// Payment entity.
    /// </summary>
    public class Payment:Entity
    {
        public string TransactionId { get; set; }

        public bool IsPaid { get; set; }

        public int Price { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int? CrashId { get; set; }

        public virtual Crash Crash { get; set; }
    }
}
