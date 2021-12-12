using System.ComponentModel.DataAnnotations;

namespace TrialProject.Shared.DTO
{
	public class CreateProjectDTO
	{

        [Required(ErrorMessage = "Project name is required field")]
        [MaxLength(50)]
        public string? name { get; set; }

        [Required(ErrorMessage = "Teacher name is required field")]
        [MaxLength(50)]
        public string? Supervisor { get; set; }

         [Required(ErrorMessage = "Teacher name is required field")]
        [MaxLength(50)]
        public string? SupervisorEmail { get; set; }


        [Required(ErrorMessage = "Short description is required")]
        public string? shortDescription { get; set; }

        [Required(ErrorMessage = "Project description is required")]
        public string? longDescription { get; set; }

        [Required(ErrorMessage = "A least one tag required")]
        public ICollection<TagsEnums>? Tags { get; set; }
        
    }
}