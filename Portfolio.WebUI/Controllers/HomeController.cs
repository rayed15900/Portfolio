using Microsoft.AspNetCore.Mvc;
using Portfolio.BusinessLogic.Interfaces;
using Portfolio.WebUI.ViewModel;

namespace Portfolio.WebUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ISkillService _skillService;

		public HomeController(ISkillService skillService)
		{
			_skillService = skillService;
		}

		public async Task<IActionResult> Index()
		{
			var model = new HomeViewModel();

			var responseSkill = await _skillService.GetAllSkillAsync();
			model.Skills = responseSkill.Data;

			return View(model);
		}
	}
}