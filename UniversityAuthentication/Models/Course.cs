namespace UniversityAuthentication.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? CourseTitle { get; set; }
        public int SeatCapacity { get; set; }
        public virtual Instructor? Instructor { get; set; }
    }
}
