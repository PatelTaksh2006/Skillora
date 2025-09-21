using System.Collections.Generic;

namespace Skillora.Repositories.Interfaces
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
