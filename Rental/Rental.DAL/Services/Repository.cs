using Rental.DAL.Abstracts;
using Rental.DAL.EF.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Services
{
    public class Repository<T> : RentRepository<T> where T : class
    {
        public Repository(RentContext context):base(context)
        {
        }

        /// <summary>
        /// Add new element into table.
        /// </summary>
        /// <param name="item"></param>
        public override void Create(T item)
        {
            _context.Entry(item).State = EntityState.Added;
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete element from table.
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(int id)
        {
            T item = _context.Set<T>().Find(id);
            if (item != null)
                _context.Entry(item).State = EntityState.Deleted;
        }

        /// <summary>
        /// Returns IEnumerable elements for predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        /// <summary>
        /// Take element for id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// Get all elements from table.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<T> Show()
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// Updates element in table.
        /// </summary>
        /// <param name="item"></param>
        public override void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
