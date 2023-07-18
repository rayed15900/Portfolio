using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.BusinessLogic.DTOs.Interfaces;
using Portfolio.BusinessLogic.Interfaces.Base;
using Portfolio.BusinessLogic.DTOs.SkillDTOs;
using Portfolio.BusinessLogic.Interfaces;
using Portfolio.Models;
using Portfolio.BusinessLogic.DTOs.EducationDTOs;
using Portfolio.BusinessLogic.Services;
using Portfolio.BusinessLogic.ValidationRules.EducationValidators;

namespace Portfolio.WebUI.Controllers
{
	[Authorize]
	public class DashboardController : Controller
	{
		private UserManager<User> _userManager;

		private readonly ISkillService _skillService;
        private readonly IValidator<SkillCreateDTO> _skillCreateDtoValidator;
        private readonly IValidator<SkillUpdateDTO> _skillUpdateDtoValidator;

        private readonly IEducationService _educationService;
        private readonly IValidator<EducationCreateDTO> _educationCreateDtoValidator;
        private readonly IValidator<EducationUpdateDTO> _educationUpdateDtoValidator;

        public DashboardController(UserManager<User> userManager, ISkillService skillService, IValidator<SkillCreateDTO> createDtoValidator, IValidator<SkillUpdateDTO> updateDtoValidator, IEducationService educationService, IValidator<EducationCreateDTO> educationCreateDtoValidator, IValidator<EducationUpdateDTO> educationUpdateDtoValidator)
		{
			_userManager = userManager;

			_skillService = skillService;
            _skillCreateDtoValidator = createDtoValidator;
            _skillUpdateDtoValidator = updateDtoValidator;

            _educationService = educationService;
            _educationCreateDtoValidator = educationCreateDtoValidator;
            _educationUpdateDtoValidator = educationUpdateDtoValidator;
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
            var validationResult = await _skillCreateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                ModelState.Remove("Name");

                foreach (var error in validationResult.Errors)
                {
                    if (error.PropertyName == "Name")
                    {
                        ModelState.AddModelError("Name", error.ErrorMessage);
                    }
                    else
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                return View(dto);
            }

            var user = await _userManager.GetUserAsync(User);
            dto.UserId = user.Id;
            var createdDto = await _skillService.CreateAsync(dto);

            return RedirectToAction("Skills");
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
            var validationResult = await _skillUpdateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                ModelState.Remove("Name");

                foreach (var error in validationResult.Errors)
                {
                    if (error.PropertyName == "Name")
                    {
                        ModelState.AddModelError("Name", error.ErrorMessage);
                    }
                    else
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                return View(dto);
            }

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
            var data = await _skillService.RemoveAsync(id);
            if(data != null)
                return RedirectToAction("Skills");
            TempData["notify"] = $"Skill Id: {id} not found";
            return RedirectToAction("Skills");
        }

        #endregion

        #region Educations

        public async Task<IActionResult> Educations()
        {
            var user = await _userManager.GetUserAsync(User);
            var dto = await _educationService.GetAllEducationsByUserIdAsync<EducationListDTO>(user.Id);
            return View(dto);
        }

        public IActionResult EducationsCreate()
        {
            var dto = new EducationCreateDTO();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> EducationsCreate(EducationCreateDTO dto)
        {
            var validationResult = await _educationCreateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                ModelState.Remove("LevelOfEducation");
                ModelState.Remove("Institution");
                ModelState.Remove("FieldOfStudy");
                ModelState.Remove("YearOfPassing");

                foreach (var error in validationResult.Errors)
                {
                    if (error.PropertyName == "LevelOfEducation")
                    {
                        ModelState.AddModelError("LevelOfEducation", error.ErrorMessage);
                    }
                    else if (error.PropertyName == "Institution")
                    {
                        ModelState.AddModelError("Institution", error.ErrorMessage);
                    }
                    else if (error.PropertyName == "FieldOfStudy")
                    {
                        ModelState.AddModelError("FieldOfStudy", error.ErrorMessage);
                    }
                    else if (error.PropertyName == "YearOfPassing")
                    {
                        ModelState.AddModelError("YearOfPassing", error.ErrorMessage);
                    }
                    else
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                return View(dto);
            }

            var user = await _userManager.GetUserAsync(User);
            dto.UserId = user.Id;
            var createdDto = await _educationService.CreateAsync(dto);

            return RedirectToAction("Educations");
        }

        public async Task<IActionResult> EducationsUpdate(int id)
        {
            var dto = await _educationService.GetByIdAsync<EducationUpdateDTO>(id);
            if (dto != null)
                return View(dto);
            TempData["notify"] = $"Education Id: {id} not found";
            return RedirectToAction("Educations");
        }

        [HttpPost]
        public async Task<IActionResult> EducationsUpdate(EducationUpdateDTO dto)
        {
            var validationResult = await _educationUpdateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                ModelState.Remove("LevelOfEducation");
                ModelState.Remove("Institution");
                ModelState.Remove("FieldOfStudy");
                ModelState.Remove("YearOfPassing");

                foreach (var error in validationResult.Errors)
                {
                    if (error.PropertyName == "LevelOfEducation")
                    {
                        ModelState.AddModelError("LevelOfEducation", error.ErrorMessage);
                    }
                    else if (error.PropertyName == "Institution")
                    {
                        ModelState.AddModelError("Institution", error.ErrorMessage);
                    }
                    else if (error.PropertyName == "FieldOfStudy")
                    {
                        ModelState.AddModelError("FieldOfStudy", error.ErrorMessage);
                    }
                    else if (error.PropertyName == "YearOfPassing")
                    {
                        ModelState.AddModelError("YearOfPassing", error.ErrorMessage);
                    }
                    else
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                return View(dto);
            }

            var user = await _userManager.GetUserAsync(User);
            dto.UserId = user.Id;
            var updatedDto = await _educationService.UpdateAsync(dto);

            if (updatedDto != null)
                return RedirectToAction("Educations");
            TempData["notify"] = $"Education Id: {dto.Id} not found";
            return View(dto);
        }

        public async Task<IActionResult> EducationsDelete(int id)
        {
            var data = await _educationService.RemoveAsync(id);
            if (data != null)
                return RedirectToAction("Educations");
            TempData["notify"] = $"Education Id: {id} not found";
            return RedirectToAction("Educations");
        }

        #endregion
    }
}