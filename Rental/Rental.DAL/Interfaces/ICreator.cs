namespace Rental.DAL.Interfaces
{
    /// <summary>
    /// Creator for entities.
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public interface ICreator<T> where T : class
    {
        /// <summary>
        /// Create item.
        /// </summary>
        /// <param name="item">New item</param>
        void Create(T item);
    }
}
