using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skillora.Controllers
{
    public class SkillController : Controller
    {
        // GET: SkillController
       
        public ActionResult Index()
        {
            return View();
        }

        // GET: SkillController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SkillController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SkillController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SkillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SkillController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SkillController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
