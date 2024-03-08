using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityAuthentication.Data;
using UniversityAuthentication.Models;

namespace UniversityAuthentication.Controllers
{
    [Authorize(Roles ="Instructor, Admin")]
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _db;
        public InstructorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Registrado"] = false;

            if(User.Identity.Name != null)
            {
                //Esta en la tabla de usuarios
                //Comprobamos si esta en la tabla de Instructores
                var instructor = await _db.Instructors.FirstOrDefaultAsync(i=>i.InstructorUser == User.Identity.Name);
                if(instructor != null)
                {
                    //Existe este instructor
                    ViewBag.InstructorId = instructor.InstructorId;
                    ViewData["Registrado"] = true;
                }
            }

            return View();
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AllProfiles()
        {
            var instructores = await _db.Instructors.ToListAsync();
            return View(instructores);
        }
        public ActionResult AddProfile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProfile(Instructor instructor)
        {
            _db.Add(instructor);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllProfiles");
        }
        public async Task<IActionResult> EditProfile(int id)
        {
            var instructorToUpadate = await _db.Instructors.FirstOrDefaultAsync(i => i.InstructorId == id);            
            return View(instructorToUpadate);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(Instructor instructor)
        {
            _db.Update(instructor);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllProfiles");
        }
    }
}
