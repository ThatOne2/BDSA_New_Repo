using System.ComponentModel.DataAnnotations;

namespace TrialProject.Shared.DTO
{
	public class CreateStudentDTO
	{
        
        [Required(ErrorMessage = "Student name is required field")]
        [MaxLength(50)]
        public string name { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
