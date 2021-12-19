namespace TrialProject.Shared;
using System.ComponentModel.DataAnnotations;

public class Project
{
    [Key]
    public int ID {get; set;}
    
    [Required(ErrorMessage = "Project name is required field")]
    [MaxLength(50)]
    public string? name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string? shortDescription { get; set; }

     [Required(ErrorMessage = "Description is required")]
     [ConcurrencyCheck]

     [StringLength(10000, MinimumLength = 150 , ErrorMessage = "Long description has to be more than 150 characters")]
    public string? longDescription { get; set; }

    public int? SupervisorID { get; set; }

    public ICollection<Tag>? Tags {get; set;} 

    [Required]
    public Status ProjectStatus { get; set; }

} 