using Skillora.Models.Entities;
using System.Collections.Generic;

namespace Skillora.Services.Interfaces
{
    public interface IStudentService
    {
        Student Get(string id);
        Student Delete(string id);
        List<Student> GetAll();
        Student Update(Student entity, string[] selectedskills,List<string> existingSkills);
        Student Add(Student entity, string[] skills);
        public List<Skill> GetAllSkills();
        public List<Job> GetAllJobs();

        public void applyJob(string studentId, List<string> jobIds);
    }
}
