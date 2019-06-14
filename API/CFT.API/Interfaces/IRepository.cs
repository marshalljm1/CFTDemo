using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CFT.API.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entry);
        void AddRange(IEnumerable<T> entries);

        void Remove(T entry);
        void RemoveRange(IEnumerable<T> entries);
    }
}
