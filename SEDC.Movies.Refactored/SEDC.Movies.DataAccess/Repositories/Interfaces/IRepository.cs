using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Movies.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
