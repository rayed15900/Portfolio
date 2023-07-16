using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.BusinessLogic.DTOs.Interfaces;
using Portfolio.BusinessLogic.DTOs.SkillDTOs;
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

		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			ViewBag.User = user;

			return View();
		}

        #region Skills

        public async Task<IActionResult> Skills()
        {
            var user = await _userManager.GetUserAsync(User);
            var dto = await _skillService.GetAllSkillsByUserIdAsync<SkillListDTO>(user.Id);
            return View(dto);
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
            var createdDto = await _skillService.CreateAsync(dto);

            if(createdDto != null)
                return RedirectToAction("Skills");
            return View(dto);
        }

        public async Task<IActionResult> SkillsUpdate(int id)
        {
            var dto = await _skillService.GetByIdAsync<SkillUpdateDTO>(id);
            if(dto != null)
                return View(dto);
            TempData["notify"] = $"Skill Id: {id} not found";
            return RedirectToAction("Skills");
        }

        [HttpPost]
        public async Task<IActionResult> SkillsUpdate(SkillUpdateDTO dto)
        {
            var user = await _userManager.GetUserAsync(User);
            dto.UserId = user.Id;
            var updatedDto = await _skillService.UpdateAsync(dto);

            if(updatedDto != null)
                return RedirectToAction("Skills");
            TempData["notify"] = $"Skill Id: {dto.Id} not found";
            return View(dto);
        }

        public async Task<IActionResult> SkillsDelete(int id)
        {
            var response = await _skillService.RemoveAsync(id);
            if(response)
                return RedirectToAction("Skills");
            TempData["notify"] = $"Skill Id: {id} not found";
            return RedirectToAction("Skills");
        }

        #endregion
    }
}