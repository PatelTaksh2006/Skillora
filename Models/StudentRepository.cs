using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillora.Models
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
            return _appDbContext.Students.ToList();
        }

        public Student GetById(string id)
        {
            return _appDbContext.Students.FirstOrDefault(x => x.Id == id);
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
