using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skillora.Models.Auth;
using Skillora.Models.Entities;
using Skillora.Models.ViewModels;
using Skillora.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Skillora.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public CompanyController(ICompanyService companyService,IMapper mapper,UserManager<AppUser> userManager)
        {
            _companyService = companyService;
            _mapper = mapper;
            _userManager = userManager;
        }
        // GET: CompanyController
        public ActionResult Index(string id)
        {
            var company = _companyService.Get(id);
            ViewData["id"] = id;
            return View(company);
            
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
        public async Task<ActionResult> Create(CreateCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Company c = _mapper.Map<Company>(model);
                Company company=_companyService.Add(c);
                var user =await _userManager.GetUserAsync(User);
                user.CompanyId = company.Id;
                var result = await _userManager.UpdateAsync(user);
                return RedirectToAction("Index","Company",new {id=company.Id});
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


        [HttpGet]
        public async Task<IActionResult> JobStatus()
        {
            var user =await _userManager.GetUserAsync(User);
            string id = user.CompanyId;
            var compnay=_companyService.Get(id);
            var jobs = compnay.Job;
            ViewData["jobs"] = jobs;
            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> JobStatus(string selectedJobId)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    string companyId = user.CompanyId;
        //    var company = _companyService.Get(companyId);

        //    var job= company.Job.Select(job=>job.CompanyId==selectedJobId);
        //    return Redirect
        //}


        [HttpGet]
        public IActionResult AdminIndex()
        {
            var companies = _companyService.GetAll().ToList();
            var model = _mapper.Map<List<CompanyViewModel>>(companies);
            return View(model);
        }


        [HttpGet]
        public IActionResult AdminApprove()
        {
            return View();
        }


    }
}
