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

        public override string ToString()
        {
            var s = "CreateProjectDTO [name = " + name;
            if (Supervisor != null) s = s + ", supervisor = " + Supervisor;
            if (SupervisorEmail != null) s = s + ", supervisorEmail = " + SupervisorEmail;
            if (shortDescription != null) s = s + ", shortDescription = \"" + shortDescription + "\"";
            if (longDescription != null) s = s + ", longDescription = \"" + longDescription + "\"";
            if (Tags != null) s = s + ", tags = " + Tags.ToString();
            s = s + "]";
            return s;
        }

    }
}