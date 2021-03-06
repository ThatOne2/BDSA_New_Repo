using System.ComponentModel.DataAnnotations;

namespace TrialProject.Shared.DTO
{
	public class ProjectDescDTO
	{
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Project name is required field")]
        [MaxLength(50)]
        public string? name { get; set; }

        [Required(ErrorMessage = "Teacher name is required field")]
        [MaxLength(50)]
        public string? SupervisorName { get; set; }

        [EmailAddress]
        public string? SupervisorEmail { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? shortDescription { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? longDescription { get; set; }

        [Required(ErrorMessage = "A least one tag required")]
        public ICollection<string>? Tags { get; set; }

        [Required]
        public Status ProjectStatus { get; set; }

        public override string ToString()
        {
            var s = "ProjectDescDTO [ID = " + ID;
            if (name != null) s = s + ", name = " + name;
            if (SupervisorName != null) s = s + ", supervisor = " + SupervisorName;
            if (SupervisorEmail != null) s = s + ", supervisorEmail = " + SupervisorEmail;
            if (shortDescription != null) s = s + ", shortDescription = \"" + shortDescription + "\"";
            if (longDescription != null) s = s + ", longDescription = \"" + longDescription + "\"";
            if (Tags != null) s = s + ", tags = " + Tags.ToString();
            s = s + ", status = " + ProjectStatus.ToString();
            s = s + "]";
            return s;
        } 
    }
}
