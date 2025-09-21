using Skillora.Models.Entities;
using System.Collections.Generic;

namespace Skillora.Repositories.Interfaces
{
    public interface IJobRepository
    {
       public  List<Job> GetAll();
        Job GetById(string id);
        void insert(Job entity,JobConstraint constraint);
        void update(Job entity);
        void delete(Job entity);
        List<Job> JobsByCompanyId(string conpmayId);

    }

}
