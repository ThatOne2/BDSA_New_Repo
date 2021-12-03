namespace TrialProject.Shared;
using System.ComponentModel.DataAnnotations;

public class Student : CurrentUser
{
    [Key]
    public int ID {get; set;}

    [Required(ErrorMessage = "Name is required field")]
    public string name { get; set; }

    [Required(ErrorMessage = "Email required")]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }


}