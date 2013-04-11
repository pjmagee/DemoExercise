using System.Collections.Generic;

namespace PurpleCubed.Domain
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Create(T entity);
        void Delete(int id);
        T Update(T entity);
    }
}