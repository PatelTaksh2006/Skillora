using Skillora.Models.Entities;
using Skillora.Repositories.Interfaces;
using Skillora.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillora.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Add a new company
        public Company Add(Company company)
        {
            // Check for unique name
            if (GetAll().Any(c => c.Name.Equals(company.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Company name must be unique.");
            }

            _unitOfWork.Company.insert(company);
            _unitOfWork.Save();
            return company;
        }

        

        // Delete a company by ID
        public Company Delete(string id)
        {
            var company = _unitOfWork.Company.GetById(id);
            if (company != null)
                _unitOfWork.Company.delete(company);

            _unitOfWork.Save();
            return company;
        }

        // Get a company by ID
        public Company Get(string id)
        {
            return _unitOfWork.Company.GetById(id);
        }

        // Get all companies
        public List<Company> GetAll()
        {
            return _unitOfWork.Company.getAll();
        }

        // Update a company
        public Company Update(Company company)
        {
            var existing = _unitOfWork.Company.GetById(company.Id);
            if (existing != null)
                _unitOfWork.Company.update(company);

            _unitOfWork.Save();
            return existing;
        }
    }
}
