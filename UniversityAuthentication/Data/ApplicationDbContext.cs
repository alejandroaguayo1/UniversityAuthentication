﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityAuthentication.Models;

namespace UniversityAuthentication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Seed();
            base.OnModelCreating(modelbuilder);
        }
    }
}
