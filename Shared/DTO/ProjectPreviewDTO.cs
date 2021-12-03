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
        
        public int ID { get; set; }

        public string name { get; set; }

        public string SupervisorName { get; set; }

        public string shortDescription { get; set; }
        
        public ICollection<Tag> Tags { get; set; }
    }
}
