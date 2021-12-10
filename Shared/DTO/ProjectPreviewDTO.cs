
namespace TrialProject.Shared.DTO
{
	public class ProjectPreviewDTO
	{
        
        public int ID { get; set; }

        public string name { get; set; }

        public string SupervisorName { get; set; }

        public string shortDescription { get; set; }
        
        public ICollection<string> Tags { get; set; }

        /* public string ToString()
        {
            var s = "[ID = " + ID 
            if (name != null) s = s + ", name = " + name;
            if (SupervisorName != null) s = s + ", supervisor = " + SupervisorName;
            if (shortDescription != null) s = s + ", shortDescription = \"" + shortDescription + "\"";
            if (Tags != null) s = s + ", tags = " + Tags.ToString();
            s = s + "]";
            return s;
        } */
    }
}
