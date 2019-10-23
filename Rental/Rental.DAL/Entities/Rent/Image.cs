namespace Rental.DAL.Entities.Rent
{
    /// <summary>
    /// Image entity.
    /// </summary>
    public class Image:Entity
    {
        public string Text { get; set; }

        public byte[] Photo { get; set; }

        public int? CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
