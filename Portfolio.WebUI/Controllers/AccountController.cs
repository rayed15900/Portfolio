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
        private readonly IUserService _userService;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
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
            if (!_userService.RegisterisValid(dto))
            {
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

			TempData["alerts"] = "Inputs are not valid";
			return View(dto);
        }

        #endregion

        #region Login

        public async Task<IActionResult> Login()
        {
            if (!_userManager.Users.Any())
            {
                var user = new User()
                {
                    Name = "Admin Panel",
                    UserName = "admin",
                };

                await _userManager.CreateAsync(user, "1234");
            }
            var dto = new UserLoginDTO();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
            if (!_userService.LoginisValid(dto))
            {
                
                return View(dto);
            }

            var user = await _userManager.FindByNameAsync(dto.Username);

            if (user == null)
            {
				TempData["alerts"] = "User not found";
				return View(dto);
            }

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true, true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            TempData["alerts"] = "Inputs are not valid";
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
