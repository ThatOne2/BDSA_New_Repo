namespace TrialProject.Shared;
using System.ComponentModel.DataAnnotations;

  public class Tag
    {
        [Key]
        public int Id {get; set;}

        [Required]
        [MaxLength(50)]
        public string Name {get; set;}
        
    }