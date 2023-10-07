using System.ComponentModel.DataAnnotations;

namespace Architecture.Models
{
    public class Application
    {
        [Key]
        public Guid ApplicationId { get; internal set; } = Guid.NewGuid();
        [Required]
        [StringLength(100)]
        public string SchoolName { get; set; }

        [Required]
        public string Referral { get; set; }

        [Required]
        public string ApplicantName { get; set; }

        [Required]
        public string ApplicantSurname { get; set; }

        [Required]
        public string ApplicantGender { get; set; }

        [Required]
        public string ApplicantAddress { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Contact number must be 10 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact number can only contain digits.")]
        public string ApplicantContactNumber { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ID number must be 13 digits.")]
        public string ApplicantIdNumber { get; set; }

        [Required]
        public int SchoolGrade { get; set; }

        [MaxLength]
        public string ConsentForm { get; set; }


        //foreign keys
        public Guid? ApplicationTypeId { get; set; }
        public ApplicationType ApplicationType { get; set; }
        public Guid? ApplicationStatusId { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
