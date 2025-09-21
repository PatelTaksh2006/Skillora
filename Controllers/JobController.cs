using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Skillora.Models.Entities;
using Skillora.Models.ViewModels;
using Skillora.Services.Implementations;
using Skillora.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Skillora.Controllers
{
    public class JobController : Controller
    {
        // GET: JobController
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            var models = _jobService.GetAll();
            return View(models);
        }

        // GET: JobController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JobController/Create
        [HttpGet("Job/Create/{companyId}")]
        public ActionResult Create(string companyId)
        {
            var job = new Job();
            job.CompanyId = companyId;
            var model = _mapper.Map<CreateJobViewModel>(job);
            var skills = _jobService.GetAllSkills();
            var selectList = new List<SelectListItem>();
            foreach (var item in skills)
            {
                selectList.Add(new SelectListItem(item.Name, item.Id));
            }
            model.skills = selectList;
            foreach (var item in model.skills)
            {
                Console.WriteLine(item);
            }
            return View(model);
        }

        // POST: JobController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateJobViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var job=_mapper.Map<Job>(model);
                    var jobCon = _mapper.Map<JobConstraint>(model);
                    _jobService.Add(job,jobCon,model.selectedSkills);
                    return Redirect("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }
            return View(model);
        }

        // GET: JobController/Edit/5
        public ActionResult Edit(string id)
        {
            var job= _jobService.Get(id);
            var skills = _jobService.GetAllSkills();
            var selectedSkills = job.SkillJobs.Select(x => new Skill()
            {
                Id = x.Skill.Id,
                Name = x.Skill.Name,
            });
            var selectList = new List<SelectListItem>();

            foreach (var item in skills)
            {
                selectList.Add(new SelectListItem(item.Name, item.Id, selectedSkills.Any(x => x.Id == item.Id)));
            }
            var model = _mapper.Map<EditJobViewModel>(job);
            _mapper.Map(job.JobConstraint,model);
            model.skills = selectList;
            model.Id = id;
            Console.WriteLine(model.CompanyId);
            return View(model);
        }

        // POST: JobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Job job = _jobService.Get(model.Id);
                    var jobConId=job.JobConstraint.Id;
                    var existingSkills = job.SkillJobs.Select(x => x.Skill.Id).ToList();
                    _mapper.Map(model, job);
                    _mapper.Map(model, job.JobConstraint);
                    job.JobConstraint.Id = jobConId;
                    _jobService.Update(job, model.selectedSkills, existingSkills);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            var allSkills = _jobService.GetAllSkills();
            var selectedSkillIds = model.selectedSkills ?? new string[0];

            model.skills = allSkills.Select(s =>
                new SelectListItem(s.Name, s.Id, selectedSkillIds.Contains(s.Id))
            ).ToList();

            return View(model);
        }

        // GET: JobController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobController/Delete/5
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var job = _jobService.Get(id);
            if (job == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<DeleteJobViewModel>(job);
            model.companyName = job.Company.Name;
            return View(model);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            var job = _jobService.Delete(id);
            if (job == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
