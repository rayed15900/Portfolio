using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.BusinessLogic.Interfaces;
using Portfolio.Models;

namespace Portfolio.WebUI.Controllers
{
	[Authorize]
	public class DashboardController : Controller
	{
		private UserManager<User> _userManager;
		private readonly ISkillService _skillService;

		public DashboardController(UserManager<User> userManager, ISkillService skillService)
		{
			_userManager = userManager;
			_skillService = skillService;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
