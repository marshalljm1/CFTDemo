using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CFT.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CFT.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public void Add(T entry)
        {
            Context.Set<T>().Add(entry);
        }

        public void AddRange(IEnumerable<T> entries)
        {
            Context.Set<T>().AddRange(entries);
        }

        public void Remove(T entry)
        {
            Context.Set<T>().Remove(entry);
        }

        public void RemoveRange(IEnumerable<T> entries)
        {
            Context.Set<T>().RemoveRange(entries);
        }
    }
}
