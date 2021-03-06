namespace TrialProject.Shared;


using System.ComponentModel.DataAnnotations;


public class Supervisor 
{
    [Key]
    public int ID {get; set;}
    
    [Required(ErrorMessage = "Name is required field")]
    public string? name { get; set; }

    [Required(ErrorMessage = "Email required")]
    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }

    public ICollection<Project>? Projects {get; set;} 

}