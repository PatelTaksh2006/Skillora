using Skillora.Models.Entities;

namespace Skillora.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Student> Student { get; }
        IGenericRepository<Company> Company { get; }
        IGenericRepository<Skill> Skill { get; }
        IJobRepository Job { get; }

        void Save();
    }
}
