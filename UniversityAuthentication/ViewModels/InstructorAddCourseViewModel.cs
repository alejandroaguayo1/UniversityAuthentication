using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityAuthentication.Models;

namespace UniversityAuthentication.ViewModels
{
    public class InstructorAddCourseViewModel
    {
        public Course? Course { get; set; }
        public Instructor? Instructor { get; set; }
        public SelectList? InstructorList { get; set; }
    }
}
