using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.DAL.Interfaces
{
    public interface IDisplayer<T> where T : class
    {
        IEnumerable<T> Show();

        T Get(int id);

        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}
