using Microsoft.AspNetCore.Identity;

namespace Portfolio.Models
{
	public class User : IdentityUser
	{
		public string Name { get; set; }

        public ICollection<Skill> Skills { get; set; }
        public ICollection<Education> Educations { get; set; }
    }
}
