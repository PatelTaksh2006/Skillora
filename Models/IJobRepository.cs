using System.Collections.Generic;

namespace Skillora.Models
{
    public interface IJobRepository:IGenericRepository<Job>
    {
        List<Job> JobsByCompanyId(string conpmayId);

    }

}
