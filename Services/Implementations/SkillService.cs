using Microsoft.AspNetCore.Razor.Language;
using Skillora.Models.Entities;
using Skillora.Repositories.Interfaces;
using Skillora.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillora.Services.Implementations
{
    public class SkillService : IGenericService<Skill>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SkillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Skill Add(Skill entity)
        {
            var skills = GetAll();
            foreach(var item in skills)
            {
                if (item.Name.Equals(entity.Name,StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Skill already exists");
                }
            }
            _unitOfWork.Skill.insert(entity);
            _unitOfWork.Save();
            return entity;
        }

        public Skill Delete(string id)
        {
            var Skill = Get(id);
            _unitOfWork.Skill.delete(Skill);
            _unitOfWork.Save();
            return Skill;
        }

        public Skill Get(string id)
        {
            return _unitOfWork.Skill.GetById(id);
        }

        public List<Skill> GetAll()
        {
            return _unitOfWork.Skill.getAll().ToList();
        }

        public Skill Update(Skill entity)
        {
            _unitOfWork.Skill.update(entity);
            _unitOfWork.Save();
            return entity;

        }
    }
}
