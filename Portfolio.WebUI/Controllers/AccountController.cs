using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.BusinessLogic.DTOs.UserDTO;
using Portfolio.BusinessLogic.Interfaces;
using Portfolio.Models;
using Portfolio.WebUI.Extension;
using static Azure.Core.HttpHeader;

namespace Portfolio.WebUI.Controllers
{
	[AutoValidateAntiforgeryToken]
	public class AccountController : Controller
    {
		private readonly IValidator<UserLoginDTO> _loginDtoValidator;
		private readonly IValidator<UserRegisterDTO> _registerDtoValidator;
		private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IValidator<UserLoginDTO> loginDtoValidator, IValidator<UserRegisterDTO> registerDtoValidator)
        {
			_userManager = userManager;
            _signInManager = signInManager;
			_loginDtoValidator = loginDtoValidator;
            _registerDtoValidator = registerDtoValidator;

		}

        #region register 

        public IActionResult Register()
        {
            var dto = new UserRegisterDTO();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO dto)
        {
			var validationResult = await _registerDtoValidator.ValidateAsync(dto);

			if (!validationResult.IsValid)
			{
				ModelState.Remove("Name");
				ModelState.Remove("Email");
				ModelState.Remove("Username");
				ModelState.Remove("Password");
				ModelState.Remove("ConfirmPassword");

				foreach (var error in validationResult.Errors)
				{
					if (error.PropertyName == "Name")
					{
						ModelState.AddModelError("Name", error.ErrorMessage);
					}
					else if (error.PropertyName == "Email")
					{
						ModelState.AddModelError("Email", error.ErrorMessage);
					}
					else if (error.PropertyName == "Username")
					{
						ModelState.AddModelError("Username", error.ErrorMessage);
					}
					else if (error.PropertyName == "Password")
					{
						ModelState.AddModelError("Password", error.ErrorMessage);
					}
					else if (error.PropertyName == "ConfirmPassword")
					{
						ModelState.AddModelError("ConfirmPassword", error.ErrorMessage);
					}
					else
					{
						ModelState.AddModelError("", error.ErrorMessage);
					}
				}
				return View(dto);
			}

			var user = new User()
            {
                Name = dto.Name.Trim(),
                Email = dto.Email.Trim(),
                UserName = dto.Username.Trim(),
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

			TempData["alerts"] = "Registration credentials invalid";
			return View(dto);
        }

        #endregion

        #region Login

        public async Task<IActionResult> Login()
        {
            //if (!_userManager.Users.Any())
            //{
            //    var user = new User()
            //    {
            //        Name = "Admin Panel",
            //        UserName = "admin",
            //    };

            //    await _userManager.CreateAsync(user, "1234");
            //}
            var dto = new UserLoginDTO();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
			var validationResult = await _loginDtoValidator.ValidateAsync(dto);

			if (!validationResult.IsValid)
			{
				ModelState.Remove("Username");
				ModelState.Remove("Password");

				foreach (var error in validationResult.Errors)
				{
					if (error.PropertyName == "Username")
					{
						ModelState.AddModelError("Username", error.ErrorMessage);
					}
					else if (error.PropertyName == "Password")
					{
						ModelState.AddModelError("Password", error.ErrorMessage);
					}
					else
					{
						ModelState.AddModelError("", error.ErrorMessage);
					}
				}
				return View(dto);
			}

            var user = await _userManager.FindByNameAsync(dto.Username);

            if (user == null)
            {
                TempData["alerts"] = "User not found.";
                return View(dto);
            }

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true, true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            TempData["alerts"] = "Login credentials invalid";
            return View(dto);
        }

        #endregion

        #region Logout

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Account/Login");
        }

        #endregion
    }
}
