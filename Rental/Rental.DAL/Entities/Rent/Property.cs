namespace Rental.DAL.Entities.Rent
{
    /// <summary>
    /// Property entity.
    /// </summary>
    public class Property:Entity
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public int? CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
