namespace Rental.BLL.DTO.Rent
{
    /// <summary>
    /// Crash data transfer object.
    /// </summary>
    public class CrashDTO : EntityDTO
    {
        public string Description { get; set; }

        public PaymentDTO Payment { get; set; }
    }
}
