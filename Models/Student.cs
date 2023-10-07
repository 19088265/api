using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Architecture.Models
{
    public class Student
    {
        [Key]
        public Guid StudentId { get; internal set; } = Guid.NewGuid();

        [Required]
        public string StudentName { get; set; }

        [Required]
        public string StudentSurname { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ID number must be 13 digits")]
        public string StudentIdNumber { get; set; }

        [Required]
        public int StudentGrade { get; set; }

        [Required]
        public string StudentGender { get; set; }

        [Required]
        public string StudentSchool { get; set; }


        public Guid? StudentTypeId { get; set; }
        public StudentType StudentType { get; set; }
    }
}
