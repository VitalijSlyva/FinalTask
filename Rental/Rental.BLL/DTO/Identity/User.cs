namespace Rental.BLL.DTO.Identity
{
    /// <summary>
    /// Information about user.
    /// </summary>
    public class User
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
