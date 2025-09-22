using Skillora.Models.Entities;
using System.Collections.Generic;

namespace Skillora.Services.Interfaces
{
    public interface IJobService
    {
        Job Get(string id);
        Job Delete(string id);
        List<Job> GetAll();
        Job Update(Job entity, string[] selectedskills, List<string> existingSkills);

        Job Add(Job entity,JobConstraint jobConstraint, string[] selectedlist);
        public Company getCompanyByJobId(string JobId);

        public List<Skill> GetAllSkills();

        public void ShortListStudents(string jobId, List<string> studentIds);

    }
}
