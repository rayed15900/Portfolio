using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.BusinessLogic.DTOs.SkillDTOs;
using Portfolio.BusinessLogic.Interfaces;
using Portfolio.Models;
using Portfolio.Utility;
using Portfolio.WebUI.Extension;

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

		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			ViewBag.User = user;

			return View();
		}

        #region Skills

        public async Task<IActionResult> Skills()
        {
            var response = await _skillService.GetAllAsync();
            return View(response.Data);
        }

        public IActionResult SkillsCreate()
        {
            var dto = new SkillCreateDTO();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> SkillsCreate(SkillCreateDTO dto)
        {
            var user = await _userManager.GetUserAsync(User);
            dto.UserId = user.Id;

            var response = await _skillService.CreateAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = "Skill Created";
                return RedirectToAction("Skills");
            }
            return this.ResponseValidation<SkillCreateDTO>(response);
        }

        public async Task<IActionResult> SkillsUpdate(int id)
        {
            var response = await _skillService.GetByIdAsync<SkillUpdateDTO>(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> SkillsUpdate(SkillUpdateDTO dto)
        {
            var user = await _userManager.GetUserAsync(User);
            dto.UserId = user.Id;

            var response = await _skillService.UpdateAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = "Skill Updated";
                return RedirectToAction("Skills");
            }
            return this.ResponseValidation<SkillUpdateDTO>(response);
        }
        
        public async Task<IActionResult> SkillsDelete(int id)
        {
            var response = await _skillService.RemoveAsync(id);
            return RedirectToAction("Skills");
        }

        #endregion
    }
}