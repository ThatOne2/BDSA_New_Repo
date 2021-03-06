namespace TrialProject.Shared;
using System.ComponentModel.DataAnnotations;

public class Student 
{
    [Key]
    public int ID {get; set;}

    [Required(ErrorMessage = "Name is required field")]
    public string? name { get; set; }

    [Required(ErrorMessage = "Email required")]
    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }

    override public string? ToString(){
        
            var s = "[ID = " + ID + ", name = " + name;
            if (Email != null) s = s + ", email = " + Email;
            s = s + "]";
            return s;
        }


}