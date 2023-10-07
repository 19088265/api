using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Models
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; internal set; } = Guid.NewGuid();

        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }

        // Add data annotations for validation
        [Required]
        [MaxLength(13)]
        public string EmployeeIdNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string EmployeeContactNumber { get; set; }

        [Required]
        [EmailAddress]
        public string EmployeeEmail { get; set; }

        [Required]
        public string EmployeeRole { get; set; }

        [Required]
        public string EmployeeGender { get; set; }

        //[ForeignKey("EmployeeType")]
        //public Guid EmployeeTypeId { get; set; }
        public Guid? EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}


