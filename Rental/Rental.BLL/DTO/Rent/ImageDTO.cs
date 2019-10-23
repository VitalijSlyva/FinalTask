namespace Rental.BLL.DTO.Rent
{
    /// <summary>
    /// Image data transfer object.
    /// </summary>
    public class ImageDTO : EntityDTO
    {
        public string Text { get; set; }

        public byte[] Photo { get; set; }
    }
}
