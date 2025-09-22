using Skillora.Models.Entities;
using Skillora.Repositories.Interfaces;
using Skillora.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillora.Services.Implementations
{
    public class StudentService:IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Student Add(Student student, string[] skills)
        {
            DateTime dob = student.DOB;
            DateTime dateTime = DateTime.Now;
            int age = dateTime.Year - dob.Year;
            if (dob.Date > dateTime.AddYears(-age)) {
                age--;
            }

            if(age<20 || age>24)
            {
                throw new Exception("Age must be in between 20-24");

            }
            
            List<Student> students = GetAll();

            if (students.Any(s => s.Email == student.Email))
            {
                throw new Exception("Email must be unique");
            }
            foreach (var item in skills)
            {
                student.SkillStudents.Add(new SkillStudent()
                {
                    SkillId = item
                });
            }
            _unitOfWork.Student.insert(student);
            _unitOfWork.Save();
            return student;
        }

        public Student Delete(string id)
        {
            Student s = _unitOfWork.Student.GetById(id);
            if(s!=null)
            _unitOfWork.Student.delete(s);
            _unitOfWork.Save();

            return s;
        }

        public Student Get(string id)
        {
            return _unitOfWork.Student.GetById(id);
        }

        public List<Student> GetAll()
        {
            return _unitOfWork.Student.getAll();
        }

        public List<Skill> GetAllSkills()
        {
            return _unitOfWork.Skill.getAll();
        }

        public Student Update(Student s, string[] selectedSkills,List<string> existingSkills)
        {

            //var existingSkills = s.SkillStudents.Select(x => x.SkillId).ToList();
            var toAdd = selectedSkills.Except(existingSkills).ToList();
            var toRemove = existingSkills.Except(selectedSkills).ToList();
            s.SkillStudents = s.SkillStudents.Where(x => !toRemove.Contains(x.SkillId)).ToList();
            foreach (var item in toAdd)
            {
                s.SkillStudents.Add(new SkillStudent()
                {
                    SkillId = item,
                    StudentId = s.Id
                });
            }
            _unitOfWork.Student.update(s);
            _unitOfWork.Save();
            return s;
        }

        public List<Job> GetAllJobs()
        {
            return _unitOfWork.Job.GetAll().ToList();
        }

        public void applyJob(string studentId, List<string> jobIds)
        {
            var student = _unitOfWork.Student.GetById(studentId);
            foreach (var item in jobIds)
            {
                student.StudentJobs.Add(new StudentJob()
                {
                    JobId = item,
                    StudentId = studentId
                });
            }

            _unitOfWork.Student.update(student);
            _unitOfWork.Save();
        }
    }
}
