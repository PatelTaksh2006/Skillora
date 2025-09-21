using Skillora.Models;
using Skillora.Models.Entities;
using System.Collections.Generic;

namespace Skillora.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        T Get(string id);
        T Delete(string id);
        List<T> GetAll();
        T Update(T entity);
        
        T Add(T entity);

    }
}
