namespace TrialProject.Shared;
using System.ComponentModel.DataAnnotations;

public class Student : CurrentUser
{
    [Key]
    public int Id {get; set;}
    [Required(ErrorMessage = "Name is required field")]
    public string name { get; set; }

    [Required(ErrorMessage = "Email required")]
    public string Email { get; set; }


}