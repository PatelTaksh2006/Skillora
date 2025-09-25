using Microsoft.EntityFrameworkCore;
using Skillora.Data;
using Skillora.Models.Entities;
using Skillora.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillora.Repositories.Implementations
{
    public class StudentRepository : IGenericRepository<Student>
    {
        private readonly AppDbContext _appDbContext;

        public StudentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void delete(Student entity)
        {
            _appDbContext.Students.Remove(entity);
        }

        public List<Student> getAll()
        {
            return _appDbContext.Students.Include(s=>s.SkillStudents).ThenInclude(s=>s.Skill).ToList();
        }

        public Student GetById(string id)
        {
            return _appDbContext.Students.Include(s=>s.SkillStudents).ThenInclude(ss => ss.Skill).Include(s=>s.StudentJobs).Include(s=>s.SelectedStudentJobs).ThenInclude(sj=>sj.Job).FirstOrDefault(x => x.Id == id);
        }

        public void insert(Student entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            _appDbContext.Students.Add(entity);
        }

        public void update(Student entity)
        {
            _appDbContext.Students.Update(entity);
        }
    }
}
