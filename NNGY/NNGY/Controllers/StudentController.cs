
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNGY.Models;

namespace NNGY.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        AppDBContext db = new AppDBContext();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

       
        // GET: Student/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var student = await db.student.Where(x => x.id == id).FirstAsync();
            return View(student);
        }

        [Authorize(Policy = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(student student)
        {
            try
            {
                int count_student = await db.student.CountAsync();
                student.id = count_student + 1;
                db.student.Add(student);
                await db.SaveChangesAsync();
                return Redirect("/Student/Details/" + student.id);
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Policy = "Administrator")]
        public async Task<ActionResult> Edit(int id)
        {
            var student = await db.student.Where(x => x.id == id).FirstAsync();

            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(student student)
        {
            try
            {
                var _student = await db.student.Where(x => x.id == student.id).FirstOrDefaultAsync();
                _student = student;
                await db.SaveChangesAsync();
                return Redirect("/Student/Details/" + student.id);
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
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
