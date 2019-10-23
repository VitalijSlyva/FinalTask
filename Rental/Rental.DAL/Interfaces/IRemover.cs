namespace Rental.DAL.Interfaces
{
    /// <summary>
    /// Remover for entities.
    /// </summary>
    public interface IRemover
    {
        /// <summary>
        /// Delete field by id.
        /// </summary>
        /// <param name="id">Item id</param>
        void Delete(int id);
    }
}
