using Microsoft.AspNetCore.Identity;

namespace Portfolio.Models
{
	public class User : IdentityUser
	{
		public string Name { get; set; }
	}
}
