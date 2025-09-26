
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Skillora.Models.Auth;
using Skillora.Models.Entities;
using Skillora.Models.ViewModels;
using Skillora.Repositories.Interfaces;
using Skillora.Services.Implementations;
using Skillora.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skillora.Controllers
{
    
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public StudentController(IStudentService studentService, IMapper mapper,UserManager<AppUser> userManager)
        {
            _studentService = studentService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Index()
        {
            //var listOfStudent = _studentService.GetAll();
            //var model = _mapper.Map<List<StudentViewModel>>(listOfStudent);
            //return View(model);

            var user =await _userManager.GetUserAsync(User);
            var studentId = user.StudentId;
            return View(_studentService.Get(studentId));

        }

        [Authorize(Roles = "Student")]

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateStudentViewModel();
            var selectList = new List<SelectListItem>();
            var skills = _studentService.GetAllSkills();
            foreach (var item in skills)
            {
                selectList.Add(new SelectListItem(item.Name, item.Id));
            }
            model.skills = selectList;
            return View(model);
        }
        [Authorize(Roles = "Student")]

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentViewModel model)
        {
            //Console.WriteLine(model);
            if (ModelState.IsValid)
            {
                try
                {
                    var Student = _mapper.Map<Student>(model);
                    var student=_studentService.Add(Student, model.selectedSKills);
                    var user = await _userManager.GetUserAsync(User);
                    user.StudentId = student.Id;
                    var result = await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Email"))
                        ModelState.AddModelError("Email", ex.Message);
                    if (ex.Message.Contains("Age"))
                        ModelState.AddModelError("DOB", ex.Message);
                }
            }
            var selectList = new List<SelectListItem>();
            var skills = _studentService.GetAllSkills();
            foreach (var item in skills)
            {
                selectList.Add(new SelectListItem(item.Name, item.Id));
            }
            model.skills = selectList;
            return View(model);
        }
        [Authorize(Roles = "Student")]

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var student = _studentService.Get(id);

            var skills = _studentService.GetAllSkills();

            var selectedSkills = student.SkillStudents.Select(x => new Skill()
            {
                Id = x.Skill.Id,
                Name = x.Skill.Name,
            });
            var selectList = new List<SelectListItem>();

            foreach (var item in skills)
            {
                selectList.Add(new SelectListItem(item.Name, item.Id, selectedSkills.Any(x => x.Id == item.Id)));
            }
            var model = _mapper.Map<EditStudentViewModel>(student);
            model.skills = selectList;
            return View(model);

        }
        [Authorize(Roles = "Student")]

        [HttpPost]
        public IActionResult Edit(EditStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Student s = _studentService.Get(model.Id);

                    var existingSkills = s.SkillStudents.Select(x => x.SkillId).ToList();
                    _mapper.Map(model, s);
                    _studentService.Update(s, model.selectedSKills, existingSkills);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            var allSkills = _studentService.GetAllSkills();
            var selectedSkillIds = model.selectedSKills ?? new string[0];

            model.skills = allSkills.Select(s =>
                new SelectListItem(s.Name, s.Id, selectedSkillIds.Contains(s.Id))
            ).ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var student = _studentService.Get(id);
            if (student == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<DeleteStudentViewModel>(student);
            return View(model);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            var student = _studentService.Get(id);
            if (student == null)
            {
                return NotFound();
            }
            _studentService.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Student")]

        [HttpGet]
        public async Task<IActionResult> JobList()
        {
            var user=await _userManager.GetUserAsync(User);
            string id = user.StudentId;
            var jobs = _studentService.GetAllJobs();
            var student = _studentService.Get(id);
            var studentSkills = student.SkillStudents.Select(ss => ss.Skill).ToList();
            List<JobListViewModel> jobListViewModels = new List<JobListViewModel>();
            foreach (var item in jobs)
            {
                if (student.StudentJobs.Any(sj => sj.JobId == item.Id && sj.applied == true))
                {
                    continue; // skip this student
                }

                var jobSkills = item.SkillJobs.Select(sj => sj.Skill).ToList();
                bool eligible = false,isApplied=false;
                
                int age = DateTime.Now.Year - student.DOB.Year;
                if (student.DOB > DateTime.Now.AddYears(-age)) age--;

                if (
                    student.Percentage10 >= item.JobConstraint.MinPercentage10 &&
                    student.Percentage12 >= item.JobConstraint.MinPercentage12 &&
                    student.Cgpa >= item.JobConstraint.MinCGPA &&
                    age >= item.JobConstraint.MinAge &&
                    age <= item.JobConstraint.MaxAge &&
                    student.SkillStudents.Any(s=>jobSkills.Select(j=>j.Id).Contains(s.SkillId))
                )
                {
                    eligible = true;
                }
                else
                {
                    eligible = false;
                }
                if (student.StudentJobs.Any(s => s.JobId == item.Id))
                {
                    isApplied = true;
                }

                

                var jobListViewModel = new JobListViewModel()
                    {
                        Id = item.Id,
                        Job=item.Title,
                        CompanyName=item.Company.Name,
                        CompanySkills = item.SkillJobs.Select(sj => sj.Skill.Name).ToList(),
                        MatchedSkills = jobSkills.Where(js => studentSkills.Any(ss => ss.Id == js.Id)).Select(js => js.Name).ToList(),
                        RemainingSkills = jobSkills.Where(js => !studentSkills.Any(ss => ss.Id == js.Id)).Select(js => js.Name).ToList(),
                        eligible = eligible,
                        applied = isApplied,

                    };
                jobListViewModels.Add(jobListViewModel);
            }
            ViewBag.studentId = student.Id;
            var sortedJobs=jobListViewModels.OrderByDescending(j => j.eligible).ThenByDescending(j => j.MatchedSkills.Count).ToList();
            return View(sortedJobs);

        }
        [Authorize(Roles = "Student")]

        [HttpPost]
        public async Task<IActionResult> JobList(List<string> selectedJobIds)
        {
            var user=await _userManager.GetUserAsync(User);
            string id = user.StudentId;
            if (selectedJobIds == null || !selectedJobIds.Any())
            {
                return RedirectToAction("JobList", new { id = id });
            }

            _studentService.applyJob(id, selectedJobIds);
            return RedirectToAction("JobList");

        }
        [Authorize(Roles = "Student")]

        [HttpGet]
        public async Task<IActionResult> offeredJob()
        {
            var user = await _userManager.GetUserAsync(User);
            string id = user.StudentId;
            var student = _studentService.Get(id);
            var offeredJobs = student.SelectedStudentJobs;
            List<OfferedJobViewModel> jobs = new List<OfferedJobViewModel>();
            foreach (var item in offeredJobs)
            {
                jobs.Add(new OfferedJobViewModel()
                {
                    JobId=item.JobId,
                    JobName=item.Job.Title,
                    status=item.Status,
                });

            }
            return View(jobs);
        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult AdminIndex()
        {
            var students = _studentService.GetAll()
    .Select(s => new StudentViewModel
    {
        Id = s.Id,
        Name = s.Name,
        Email = s.Email,
        DOB = s.DOB,
        Github = s.Github,
        Cgpa = s.Cgpa,
        Phone = s.Phone,
        Percentage10 = s.Percentage10,
        Percentage12 = s.Percentage12,
        Skills = s.SkillStudents.Select(sk => sk.Skill.Name).ToList()
    })
    .ToList();

            return View(students);
        }
    }
}
