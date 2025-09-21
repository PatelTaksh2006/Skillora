
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Skillora.Models.Entities;
using Skillora.Models.ViewModels;
using Skillora.Repositories.Interfaces;
using Skillora.Services.Implementations;
using Skillora.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skillora.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var listOfStudent = _studentService.GetAll();
            var model = _mapper.Map<List<StudentViewModel>>(listOfStudent);
            return View(model);

        }

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

        [HttpPost]
        public IActionResult Create(CreateStudentViewModel model)
        {
            //Console.WriteLine(model);
            if (ModelState.IsValid)
            {
                try
                {
                    var Student = _mapper.Map<Student>(model);
                    _studentService.Add(Student, model.selectedSKills);

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

        [HttpPost]
        public IActionResult Edit(EditStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student s = _studentService.Get(model.Id);

                var existingSkills = s.SkillStudents.Select(x => x.SkillId).ToList();
                _mapper.Map(model, s);
                _studentService.Update(s, model.selectedSKills, existingSkills);
                return RedirectToAction("Index");
            }
            var allSkills = _studentService.GetAllSkills();
            var selectedSkillIds = model.selectedSKills ?? new string[0];

            model.skills = allSkills.Select(s =>
                new SelectListItem(s.Name, s.Id, selectedSkillIds.Contains(s.Id))
            ).ToList();
            return View(model);
        }

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


        [HttpGet]
        public IActionResult JobList(string id)
        {
            var jobs = _studentService.GetAllJobs();
            var student = _studentService.Get(id);
            var studentSkills = student.SkillStudents.Select(ss => ss.Skill).ToList();
            List<JobListViewModel> jobListViewModels = new List<JobListViewModel>();
            foreach (var item in jobs)
            {
                var jobSkills = item.SkillJobs.Select(sj => sj.Skill).ToList();
                bool eligible = false;
                int age = DateTime.Now.Year - student.DOB.Year;
                if (student.DOB > DateTime.Now.AddYears(-age)) age--;

                if (
                    student.Percentage10 >= item.JobConstraint.MinPercentage10 &&
                    student.Percentage12 >= item.JobConstraint.MinPercentage12 &&
                    student.Cgpa >= item.JobConstraint.MinCGPA &&
                    age >= item.JobConstraint.MinAge &&
                    age <= item.JobConstraint.MaxAge
                )
                {
                    eligible = true;
                }
                else
                {
                    eligible = false;
                }
                var jobListViewModel = new JobListViewModel()
                {
                    Id = item.Id,
                    CompanySkills = item.SkillJobs.Select(sj => sj.Skill.Name).ToList(),
                    MatchedSkills = jobSkills.Where(js => studentSkills.Any(ss => ss.Id == js.Id)).Select(js => js.Name).ToList(),
                    RemainingSkills = jobSkills.Where(js => !studentSkills.Any(ss => ss.Id == js.Id)).Select(js => js.Name).ToList(),
                    eligible = eligible
                };
                jobListViewModels.Add(jobListViewModel);
            }
            ViewBag.studentId = student.Id;
            return View(jobListViewModels);

        }

        [HttpPost]
        public IActionResult JobList(string id, List<string> selectedJobIds)
        {
            if (selectedJobIds == null || !selectedJobIds.Any())
            {
                TempData["Message"] = "No jobs selected!";
                return RedirectToAction("JobList", new { id = id });
            }

            _studentService.applyJob(id, selectedJobIds);
            return RedirectToAction("Index");

        }
    }
}
