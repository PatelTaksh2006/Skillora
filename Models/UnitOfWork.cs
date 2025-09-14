using System.Runtime.CompilerServices;

namespace Skillora.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private IGenericRepository<Student> _student ;

        private IGenericRepository<Company> _company ;

        private IGenericRepository<Skill> _skill ;

        private IJobRepository _job ;
        
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IGenericRepository<Student> Student
        {
            get
            {
                return _student = _student ?? new StudentRepository(_appDbContext);
            }
        }

        public IGenericRepository<Company> Company
        {
            get
            {
                return _company = _company ?? new CompanyRepository(_appDbContext);
            }
        }

        public IGenericRepository<Skill> Skill
        {
            get
            {
                return _skill = _skill ?? new SkillRepository(_appDbContext);
            }
        }

        public IJobRepository Job
        {
            get
            {
                return _job = _job ?? new JobRepository(_appDbContext);
            }
        }
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
