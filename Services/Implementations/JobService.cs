using Skillora.Models.Entities;
using Skillora.Repositories.Interfaces;
using Skillora.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillora.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Add a new job
        public Job Add(Job job, JobConstraint jobConstraint, string[] selectedlist)
        {
            // Ensure company exists before adding job
            var company = _unitOfWork.Company.GetById(job.CompanyId);
            //Console.WriteLine(job.CompanyId);
            if (company == null)
            {
                throw new Exception("Cannot create a job for a non-existing company.");
            }
            if (jobConstraint.MinAge > jobConstraint.MaxAge)
            {
                throw new Exception("Min age must be less than the max age");
            }
            foreach (var item in selectedlist)
            {
                job.SkillJobs.Add(new SkillJob()
                {
                    SkillId = item
                });
            }
            _unitOfWork.Job.insert(job, jobConstraint);
            _unitOfWork.Save();
            return job;
        }

        // Delete a job by ID
        public Job Delete(string id)
        {
            var job = _unitOfWork.Job.GetById(id);
            if (job != null)
            {
                _unitOfWork.Job.delete(job);
                _unitOfWork.Save();
            }
            return job;
        }

        // Get a job by ID
        public Job Get(string id)
        {
            return _unitOfWork.Job.GetById(id);
        }

        // Get all jobs
        public List<Job> GetAll()
        {
            return _unitOfWork.Job.GetAll();
        }

        public List<Skill> GetAllSkills()
        {
            return _unitOfWork.Skill.getAll();
        }

        public Company getCompanyByJobId(string JobId)
        {
            var job = _unitOfWork.Job.GetById(JobId);
            return job != null ? job.Company : null;
        }

        // Update a job
        

        public Job Update(Job job, string[] selectedSkills, List<string> existingSkills)
        {
            var company = _unitOfWork.Company.GetById(job.CompanyId);
            //Console.WriteLine(job.CompanyId);
            if (company == null)
            {
                throw new Exception("Cannot create a job for a non-existing company.");
            }
            if (job.JobConstraint.MinAge > job.JobConstraint.MaxAge)
            {
                throw new Exception("Min age must be less than the max age");
            }
            var toAdd = selectedSkills.Except(existingSkills).ToList();
            var toRemove = existingSkills.Except(selectedSkills).ToList();

            job.SkillJobs = job.SkillJobs.Where(x => !toRemove.Contains(x.Skill.Id)).ToList();

            foreach (var item in toAdd)
            {
                job.SkillJobs.Add(new SkillJob()
                {
                    SkillId = item,
                    JobId = job.Id
                });
            }

            _unitOfWork.Job.update(job);
            //_unitOfWork.Job.update(jobConstraint);
            _unitOfWork.Save();

            return job;
        }

        
    }
}
