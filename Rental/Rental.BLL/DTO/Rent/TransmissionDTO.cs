namespace Rental.BLL.DTO.Rent
{
    /// <summary>
    /// Transmission data transfer object.
    /// </summary>
    public class TransmissionDTO : EntityDTO
    {
        public string Category { get; set; }

        public int Count { get; set; }
    }
}
