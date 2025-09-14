using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillora.Models
{
    public class CompanyRepository : IGenericRepository<Company>
    {
        private readonly AppDbContext _appDbContext;

        public CompanyRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void delete(Company entity)
        {
            _appDbContext.Companies.Remove(entity);
        }

        public List<Company> getAll()
        {
            return _appDbContext.Companies.ToList();
        }

        public Company GetById(string id)
        {
            return _appDbContext.Companies.FirstOrDefault(x => x.Id == id);
        }

        public void insert(Company entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            _appDbContext.Companies.Add(entity);
        }

        public void update(Company entity)
        {
            _appDbContext.Companies.Update(entity);
        }
    }
}
