using System.ComponentModel.DataAnnotations;

namespace TrialProject.Shared.DTO
{
	public class StudentDescDTO
	{
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Student name is required field")]
        [MaxLength(50)]
        public string? name { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        override public string? ToString()
        {
            var s = "StudentDescDTO [ID = " + ID + ", name = " + name;
            if (Email != null) s = s + ", email = " + Email;
            s = s + "]";
            return s;
        }
    }
}
