namespace Rental.BLL.DTO.Rent
{
    /// <summary>
    /// Payment data transfer object.
    /// </summary>
    public class PaymentDTO : EntityDTO
    {
        public string TransactionId { get; set; }

        public bool IsPaid { get; set; }

        public int Price { get; set; }
    }
}
