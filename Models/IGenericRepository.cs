using System.Collections.Generic;

namespace Skillora.Models
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> getAll();
        T GetById(string id);
        void insert(T entity);
        void update(T entity);
        void delete(T entity);

    }
}
