using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityAuthentication.Data;
using UniversityAuthentication.Models;

namespace UniversityAuthentication.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Registrado"] = false;

            if (User.Identity.Name != null)
            {
                //Esta en la tabla de usuarios
                //Comprobamos si esta en la tabla de Students
                var student = await _db.Students.FirstOrDefaultAsync(i => i.StudentUser == User.Identity.Name);
                if (student != null)
                {
                    //Existe este Student
                    ViewBag.StudentId = student.StudentId;
                    ViewData["Registrado"] = true;
                }
            }
            return View();
        }
        [Authorize(Roles = "Student")]
        public ActionResult AddProfile()
        {
            var currentUserId = User.Identity.Name;
            Student student = new Student();
            student.StudentUser = currentUserId;
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> AddProfile(Student student)
        {
            _db.Add(student);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> EditProfile(int id)
        {
            var studentToUpadate = await _db.Students.FirstOrDefaultAsync(i => i.StudentId == id);
            return View(studentToUpadate);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(Student student)
        {
            _db.Update(student);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
