using Microsoft.AspNetCore.Mvc;

namespace Portfolio.WebUI.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			//var model = new HomeViewModel();

			//var responseSkill = await _skillService.GetAllAsync();
			//model.Skills = responseSkill.Data;

			//return View(model);
			return View();
		}
	}
}