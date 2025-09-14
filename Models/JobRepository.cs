using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillora.Models
{

    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _appDbContext;

        public JobRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void delete(Job entity)
        {
            var constraint = _appDbContext.JobConstraints.FirstOrDefault(c => c.JobId == entity.Id);
            if (constraint != null)
            {
                _appDbContext.JobConstraints.Remove(constraint);
            }
            _appDbContext.Jobs.Remove(entity);
        }

        public List<Job> getAll()
        {
            return _appDbContext.Jobs.Include(j => j.JobConstraint).ToList();
        }

        public Job GetById(string id)
        {
            return _appDbContext.Jobs.Include(j => j.JobConstraint).FirstOrDefault(j => j.Id == id);
        }

        public void insert(Job entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            _appDbContext.Jobs.Add(entity);
        }

        public List<Job> JobsByCompanyId(string conpmayId)
        {
            return _appDbContext.Jobs.Include(j => j.JobConstraint).Where(j => j.CompanyId == conpmayId).ToList();
        }

        public void update(Job entity)
        {
            _appDbContext.Jobs.Update(entity);

            if (entity.JobConstraint != null)
            {
                if (string.IsNullOrEmpty(entity.JobConstraint.Id))
                {

                    entity.JobConstraint.Id = Guid.NewGuid().ToString();
                    entity.JobConstraint.JobId = entity.Id;
                    _appDbContext.JobConstraints.Add(entity.JobConstraint);
                }
                else
                {
                    _appDbContext.JobConstraints.Update(entity.JobConstraint);
                }
            }

        }
    }
}
