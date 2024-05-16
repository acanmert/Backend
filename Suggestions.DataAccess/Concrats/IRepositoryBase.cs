using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.DataAccess.Concrats
{
    public interface IRepositoryBase<T>
    {
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
        IQueryable<T> FindAll();
        

    }
}
