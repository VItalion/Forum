using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Find(int id);
        IEnumerable<T> Get(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
