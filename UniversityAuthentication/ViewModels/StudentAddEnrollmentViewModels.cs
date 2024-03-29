﻿using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityAuthentication.Models;

namespace UniversityAuthentication.ViewModels
{
    public class StudentAddEnrollmentViewModels
    {
        public Student? Student { get; set; }
        public Course? Course { get; set; }
        public SelectList? CourseList { get; set; }
    }
}
