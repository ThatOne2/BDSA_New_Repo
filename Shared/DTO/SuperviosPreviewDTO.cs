using System.ComponentModel.DataAnnotations;

namespace TrialProject.Shared.DTO
{
	public class SuperviosPreviewDTO
	{
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Supervisor name is required field")]
        [MaxLength(50)]
        public string? name { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        override public string ToString()
        {
            var s = "[ID = " + ID + ", name = " + name;
            if (Email != null) s = s + ", email = " + Email;
            s = s + "]";
            return s;
        }
    }
}
