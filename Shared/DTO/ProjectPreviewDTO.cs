using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TrialProject.Shared.DTO
{
	public class ProjectPreviewDTO
	{
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Project name is required field")]
        [MaxLength(50)]
        public string name { get; set; }

        [Required(ErrorMessage = "Teacher name is required field")]
        [MaxLength(50)]
        public string SupervisorID { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string shortDescription { get; set; }

        [Required(ErrorMessage = "A least one tag required")]
        public ICollection<Tag> Tags { get; set; }
    }
}
