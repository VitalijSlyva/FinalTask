using System;
using System.Collections.Generic;

namespace Rental.DAL.Interfaces
{
    /// <summary>
    /// Displayer for entity items.
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public interface IDisplayer<T> where T : class
    {
        /// <summary>
        /// Show all items.
        /// </summary>
        /// <returns>Items</returns>
        IEnumerable<T> Show();

        /// <summary>
        /// Get element by id.
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Item</returns>
        T Get(int id);

        /// <summary>
        /// Find items by condition.
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns>Found items</returns>
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}
