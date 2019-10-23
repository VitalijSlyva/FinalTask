namespace Rental.DAL.Interfaces
{
    /// <summary>
    /// Updater for entities.
    /// </summary>
    /// <typeparam name="T">Entity class</typeparam>
    public interface IUpdateer<T> where T : class
    {
        /// <summary>
        /// Update field.
        /// </summary>
        /// <param name="item">Updated item.</param>
        void Update(T item);
    }
}
