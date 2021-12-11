using System.ComponentModel.DataAnnotations;

namespace TrialProject.Shared.DTO
{
	public class StudentPreviewDTO
	{
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Student name is required field")]
        [MaxLength(50)]
        public string? name { get; set; }

        override public string ToString()
        {
            var s = "[ID = " + ID + ", name = " + name + "]";
            return s;
        }
    }
}
