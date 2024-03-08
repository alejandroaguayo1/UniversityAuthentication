using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UniversityAuthentication.Controllers
{
    [Authorize(Roles ="Instructor, Admin")]
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
