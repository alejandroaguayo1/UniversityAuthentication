using System.ComponentModel.DataAnnotations;

namespace UniversityAuthentication.Models
{
    public enum LetterGrade
    {
        A,B,C,D,F,I,W,P
    }
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Course? Course { get; set; }
        [DisplayFormat(NullDisplayText ="No grade")]
        public LetterGrade LetterGrade { get; set; }
    }
}
