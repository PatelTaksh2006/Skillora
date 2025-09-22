using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillora.Models.Entities;
using Skillora.Models.ViewModels;
using Skillora.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace Skillora.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService companyService,IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET: CompanyController
        public ActionResult Index()
        {
            var companies = _companyService.GetAll();
            var model=_mapper.Map<List<CompanyViewModel>>(companies);
            return View(model);
        }

        // GET: CompanyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Company c = _mapper.Map<Company>(model);
                _companyService.Add(c);
                return Redirect("Index");
            }
            return View();
        }

        // GET: CompanyController/Edit/5
        public ActionResult Edit(string id)
        {
            var company=_companyService.Get(id);
            var model=_mapper.Map<EditCompanyViewModel>(company);
            return View(model);
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCompanyViewModel model)
        {
            try
            {
                var company=_mapper.Map<Company>(model);
                _companyService.Update(company);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyController/Delete/5
        public ActionResult Delete(string id)
        {
            var company = _companyService.Get(id);
            if(company == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<CompanyViewModel>(company);
            return View(model);
        }

        // POST: CompanyController/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            var company = _companyService.Delete(id);
            if(company==null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        
    }
}
