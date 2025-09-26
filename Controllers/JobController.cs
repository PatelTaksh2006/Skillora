using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Skillora.Models.Auth;
using Skillora.Models.Entities;
using Skillora.Models.ViewModels;
using Skillora.Services.Implementations;
using Skillora.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Skillora.Controllers
{

    public class JobController : Controller
    {
        // GET: JobController
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public JobController(IJobService jobService, IMapper mapper,UserManager<AppUser> userManager)
        {
            _jobService = jobService;
            _mapper = mapper;
            _userManager = userManager;
        }
        [Authorize(Roles = "Company")]
       
        [HttpGet("Job/DisplayByCompany")]
        public async Task<ActionResult> DisplayByCompany()
        {
            var user = await _userManager.GetUserAsync(User);
            string companyId = user.CompanyId;
            var models = _jobService.GetAll()
                .Where(j => j.CompanyId == companyId)
                .Select(j => (j))
                .ToList();
            ViewData["id"] = companyId;
            
            return View(models);
        }

        // GET: JobController/Details/5



        // GET: JobController/Create

        [Authorize(Roles = "Company")]
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
            ViewData["id"] = companyId;
            return View(model);
        }

        // POST: JobController/Create
        [Authorize(Roles = "Company")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var job = _mapper.Map<Job>(model);
                    var jobCon = _mapper.Map<JobConstraint>(model);
                    _jobService.Add(job, jobCon, model.selectedSkills);
                    return RedirectToAction("DisplayByCompany", "Job", new { companyId = model.CompanyId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            var skills = _jobService.GetAllSkills();
            var selectList = new List<SelectListItem>();
            foreach (var item in skills)
            {
                selectList.Add(new SelectListItem(item.Name, item.Id));
            }
            model.skills = selectList;
            return View(model);
        }

        // GET: JobController/Edit/5
        [Authorize(Roles = "Company")]
        public ActionResult Edit(string id)
        {
            var job = _jobService.Get(id);
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
            _mapper.Map(job.JobConstraint, model);
            model.skills = selectList;
            model.Id = id;
            Console.WriteLine(model.CompanyId);
            return View(model);
        }

        // POST: JobController/Edit/5
        [Authorize(Roles = "Company")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditJobViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Job job = _jobService.Get(model.Id);
                    var jobConId = job.JobConstraint.Id;
                    var existingSkills = job.SkillJobs.Select(x => x.Skill.Id).ToList();
                    _mapper.Map(model, job);
                    _mapper.Map(model, job.JobConstraint);
                    job.JobConstraint.Id = jobConId;
                    _jobService.Update(job, model.selectedSkills, existingSkills);

                    return RedirectToAction("DisplayByCompany","Job");
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
        [Authorize(Roles = "Company")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobController/Delete/5
        [Authorize]
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


        [Authorize(Roles = "Company")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            var job = _jobService.Delete(id);
            if (job == null)
            {
                return NotFound();
            }
            return RedirectToAction("DisplayByCompany");
        }

        [Authorize(Roles = "Company")]
        [HttpGet]
        public IActionResult GetApplyList(string id)
        {
            if(id==null)
            {
                return RedirectToAction("JobStatus", "Company");
            }
            var job = _jobService.Get(id);
            var jobSkills = job.SkillJobs.Select(sj=>sj.Skill);
            if (job == null)
                return NotFound();

            List<ApplyStudentViewModel> applyStudentViewModels = new List<ApplyStudentViewModel>();
            if (job.StudentJobs != null && job.StudentJobs.Any(sj => sj.applied == false))
            {
                ViewData["result"] = "applied";
                List<StudentJob> students = job.StudentJobs;



                foreach (var item in students)
                {

                    ApplyStudentViewModel apl = new ApplyStudentViewModel()
                    {
                        StudentId = item.StudentId,
                        Name = item.Student.Name,
                        Email = item.Student.Email,
                        Phone = item.Student.Phone,
                        Github = item.Student.Github,
                        Cgpa = item.Student.Cgpa,
                        Percentage10 = item.Student.Percentage10,
                        Percentage12 = item.Student.Percentage12,
                        Skills = item.Student.SkillStudents.Select(s => s.Skill.Name).ToList(),
                        matched= item.Student.SkillStudents.Where(s => jobSkills.Any(js=>js.Id==s.Skill.Id)).Select(s=>s.Skill.Name).ToList()
                    };
                    applyStudentViewModels.Add(apl);
                }

            }
            else
            {
                ViewData["result"] = "offered";
                List<SelectedStudentJob> students = job.SelectedStudentJobs;



                foreach (var item in students)
                {
                    if (item.Status == false) continue;
                    ApplyStudentViewModel apl = new ApplyStudentViewModel()
                    {
                        StudentId = item.StudentId,
                        Name = item.Student.Name,
                        Email = item.Student.Email,
                        Phone = item.Student.Phone,
                        Github = item.Student.Github,
                        Cgpa = item.Student.Cgpa,
                        Percentage10 = item.Student.Percentage10,
                        Percentage12 = item.Student.Percentage12,
                        Skills = item.Student.SkillStudents.Select(s => s.Skill.Name).ToList(),
                        matched = item.Student.SkillStudents.Where(s => jobSkills.Any(js => js.Id == s.Skill.Id)).Select(s => s.Skill.Name).ToList()

                    };
                    applyStudentViewModels.Add(apl);
                }
            }
            //needed
            ViewData["id"] = id;
            var sortedModels = applyStudentViewModels.OrderByDescending(a=>a.matched.Count).ThenByDescending(a=>a.Cgpa).ToList();
            return View(sortedModels);
        }

        [Authorize(Roles = "Company")]
        [HttpPost]
        public IActionResult GetApplyList(string id, List<string> selectedStudents)
        {
            if (selectedStudents == null)
            {
                ModelState.AddModelError(string.Empty, "Please select at least one student.");
                ViewData["id"] = id;
                return RedirectToAction("GetApplyList", new { id = id });
            }

            Console.WriteLine(id);
            _jobService.ShortListStudents(id, selectedStudents);
            return RedirectToAction("JobStatus", "Company");

        }

        [Authorize(Roles = "Company,Student")]
        [HttpGet]
        public IActionResult JobDetails(string id,string returnUrl)
        {
            var job = _jobService.Get(id);
            if (job == null)
                return NotFound();
            JobViewModel model = _mapper.Map<JobViewModel>(job.JobConstraint);
            _mapper.Map(job, model);
            model.CompanyName = job.Company.Name;
            model.Skills = job.SkillJobs.Select(sj => sj.Skill.Name).ToList();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);

        }
    }
}
