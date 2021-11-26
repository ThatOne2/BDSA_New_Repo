namespace TrialProject.Shared;
using System.ComponentModel.DataAnnotations;

public class Project
{
    [Key]
    public int Id {get; set;}
    
    [Required(ErrorMessage = "Project name is required field")]
    [MaxLength(50)]
    public string name { get; set; }

    [Required(ErrorMessage = "Teacher name is required field")]
    [MaxLength(50)]
    public string supervisor { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string shortDescription { get; set; }

     [Required(ErrorMessage = "Description is required")]
    public string longDescription { get; set; }

    [Required(ErrorMessage = "A least one tag required")]
    public ICollection<Tag> Tags {get; set;} 

    [Required]
    public Status ProjectStatus { get; set; }


} 