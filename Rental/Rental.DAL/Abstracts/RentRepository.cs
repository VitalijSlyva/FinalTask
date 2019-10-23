using Rental.DAL.EF.Contexts;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace Rental.DAL.Abstracts
{
    /// <summary>
    /// Template for working with RerntContext entities.
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public abstract class RentRepository<T> : IDisplayer<T>, IRemover, IUpdateer<T>, ICreator<T> where T : class
    {
        protected RentContext _context;

        /// <summary>
        /// Context for repository. 
        /// </summary>
        /// <param name="context">Database context</param>
        public RentRepository(RentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Сreate a new field in the table.
        /// </summary>
        /// <param name="item">New item</param>
        public abstract void Create(T item);

        /// <summary>
        /// Delete field from table.
        /// </summary>
        /// <param name="id">Item id</param>
        public abstract void Delete(int id);

        /// <summary>
        /// Looking for fields in table.
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns>Found items</returns>
        public abstract IEnumerable<T> Find(Func<T, bool> predicate);

        /// <summary>
        /// Get field by id.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <returns>Item</returns>
        public abstract T Get(int id);

        /// <summary>
        /// Get all fields from table.
        /// </summary>
        /// <returns>Items.</returns>
        public abstract IEnumerable<T> Show();

        /// <summary>
        /// Update field in table.
        /// </summary>
        /// <param name="item">Updated item</param>
        public abstract void Update(T item);
    }
}
