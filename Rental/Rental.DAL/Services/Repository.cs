using Rental.DAL.Abstracts;
using Rental.DAL.EF.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Rental.DAL.Services
{
    /// <summary>
    /// Template of repository for work with entities
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public class Repository<T> : RentRepository<T> where T : class
    {
        public Repository(RentContext context):base(context)
        {

        }

        /// <summary>
        /// Add new item in table.
        /// </summary>
        /// <param name="item">New item</param>
        public override void Create(T item)
        {
            _context.Entry(item).State = EntityState.Added;
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete element from table.
        /// </summary>
        /// <param name="id">Item id</param>
        public override void Delete(int id)
        {
            T item = _context.Set<T>().Find(id);
            if (item != null)
                _context.Entry(item).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        /// <summary>
        /// Returns items by condition.
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns>Itmes</returns>
        public override IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        /// <summary>
        /// Take element by id.
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Item</returns>
        public override T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// Get all items.
        /// </summary>
        /// <returns>Items</returns>
        public override IEnumerable<T> Show()
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// Updates item.
        /// </summary>
        /// <param name="item">Updated item</param>
        public override void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
