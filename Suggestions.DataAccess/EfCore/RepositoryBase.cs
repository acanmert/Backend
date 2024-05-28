using Suggestions.DataAccess.Concrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.DataAccess.EfCore
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext _context;
        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);

            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;

        }

        public IQueryable<T> FindAll()
        {

            return _context.Set<T>();
        }



        public T Update(T entity)
        {
              _context.Set<T>().Update(entity);
            
            return entity;
        }
    }
}
