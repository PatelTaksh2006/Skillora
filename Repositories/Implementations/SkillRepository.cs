using Skillora.Data;
using Skillora.Models.Entities;
using Skillora.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillora.Repositories.Implementations
{
    public class SkillRepository : IGenericRepository<Skill>
    {
        private readonly AppDbContext _appDbContext;

        public SkillRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void delete(Skill entity)
        {
            _appDbContext.Skills.Remove(entity);
        }

        public List<Skill> getAll()
        {
            return _appDbContext.Skills.ToList();
        }

        public Skill GetById(string id)
        {
            return _appDbContext.Skills.FirstOrDefault(x => x.Id == id);
        }

        public void insert(Skill entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            _appDbContext.Skills.Add(entity);
        }

        public void update(Skill entity)
        {
            _appDbContext.Skills.Update(entity);
        }
    }
}
