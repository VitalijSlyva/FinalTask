namespace Rental.WEB.Interfaces
{
    /// <summary>
    /// Interface for create action logs.
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// Create action log.
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="authorId">Author</param>
        void CreateLog(string action, string authorId);
    }
}