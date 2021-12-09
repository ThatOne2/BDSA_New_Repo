using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TrialProject.Shared.DTO
{
	public class StudentPreviewDTO
	{
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Student name is required field")]
        [MaxLength(50)]
        public string name { get; set; }

        public string ToString()
        {
            var s = "[ID = " + ID + ", name = " + name + "]";
            return s;
        }
    }
}
