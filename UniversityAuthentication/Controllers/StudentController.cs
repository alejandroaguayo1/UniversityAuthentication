using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityAuthentication.Data;
using UniversityAuthentication.Models;
using UniversityAuthentication.ViewModels;

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
        public async Task<IActionResult> EnrollCourse()
        {
            var currentUserId = User.Identity.Name;
            Student studentToShow = await _db.Students.Where(s => s.StudentUser == currentUserId).FirstOrDefaultAsync();
            var courseDisplay = await _db.Courses.Select(x => new
            {
                Id = x.CourseId,
                Value = x.CourseTitle
            }).ToListAsync();
            StudentAddEnrollmentViewModels vm = new StudentAddEnrollmentViewModels();
            vm.CourseList = new SelectList(courseDisplay, "Id", "Value");
            vm.Student = studentToShow;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> EnrollCourse(StudentAddEnrollmentViewModels vm)   
        {
                var course = await _db.Courses.FirstOrDefaultAsync(i => i.CourseId == vm.Course.CourseId);
                vm.Course = course;
                _db.Add(vm.Course);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Student");
        }

    }
}
