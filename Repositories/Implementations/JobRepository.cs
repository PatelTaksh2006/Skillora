using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Skillora.Data;
using Skillora.Models.Entities;
using Skillora.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillora.Repositories.Implementations
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

        public List<Job> GetAll()
        {
            return _appDbContext.Jobs.Include(j=>j.Company).Include(j=>j.JobConstraint).Include(j=>j.SkillJobs).ThenInclude(sj=>sj.Skill).Include(j=>j.StudentJobs).ThenInclude(sj=>sj.Student).ToList();
        }

        

        public Job GetById(string id)
        {
            return _appDbContext.Jobs.Include(j=>j.Company).Include(j => j.SkillJobs).ThenInclude(sj => sj.Skill).Include(j => j.JobConstraint).Include(j => j.StudentJobs).ThenInclude(sj => sj.Student).Include(s=>s.SelectedStudentJobs).ThenInclude(ssj=>ssj.Student).ThenInclude(s=>s.SkillStudents).ThenInclude(s=>s.Skill).FirstOrDefault(j => j.Id == id);
        }

        public void insert(Job entity,JobConstraint jobConstraint)
        {
            entity.Id = Guid.NewGuid().ToString();
            jobConstraint.Id = Guid.NewGuid().ToString();
            _appDbContext.Jobs.Add(entity);
            jobConstraint.JobId = entity.Id;
            _appDbContext.JobConstraints.Add(jobConstraint);
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
                _appDbContext.JobConstraints.Update(entity.JobConstraint);

            }
        }
    }
}
