using Rental.DAL.EF.Contexts;
using Rental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Abstracts
{
    public abstract class RentRepository<T> : IDisplayer<T>, IRemover, IUpdateer<T>, ICreator<T> where T : class
    {
        protected RentContext _context;

        public RentRepository(RentContext context)
        {
            _context = context;
        }

        public abstract void Create(T item);

        public abstract void Delete(int id);

        public abstract IEnumerable<T> Find(Func<T, bool> predicate);

        public abstract T Get(int id);

        public abstract IEnumerable<T> Show();

        public abstract void Update(T item);
    }
}
