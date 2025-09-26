
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skillora.Models.Entities;
using Skillora.Repositories.Interfaces;
using Skillora.Services.Interfaces;
using System;
using System.Linq;

namespace Skillora.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SkillController : Controller
    {
        // GET: SkillController
        private readonly IGenericService<Skill> _skillService;
       
        public SkillController(IGenericService<Skill> skillService)
        {
            _skillService = skillService;
        }

        public ActionResult AdminIndex()
        {
            return View(_skillService.GetAll().ToList());
        }

        // GET: SkillController/Details/5
        public ActionResult Details(string id)
        {
            return View(_skillService.Get(id));
        }

        // GET: SkillController/Create
        public ActionResult Create()
        {
            var model = new Skill();
            return View(model);
        }

        // POST: SkillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Skill model)
        {
            if(ModelState.IsValid)
            {
                try {
                    _skillService.Add(model);
                    return RedirectToAction("AdminIndex");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }
            return View();
        }

        // GET: SkillController/Edit/5
        public ActionResult Edit(string id)
        {
            var model = _skillService.Get(id);
            return View(model);
        }

        // POST: SkillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Skill model)
        {
            if(ModelState.IsValid)
            {
                _skillService.Update(model);
                return RedirectToAction("AdminIndex");
            }
            return View();
        }

        // GET: SkillController/Delete/5
        public ActionResult Delete(string id)
        {
            var model = _skillService.Get(id);
            return View();
        }

        // POST: SkillController/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            _skillService.Delete(id);
            return RedirectToAction("AdminIndex");
        }
    }
}
