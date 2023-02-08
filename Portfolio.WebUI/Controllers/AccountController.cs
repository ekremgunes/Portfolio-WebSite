using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Business.Interfaces;
using Portfolio.Dtos;
using Portfolio.Entities;
using Portfolio.WebUI.Models;

namespace Portfolio.WebUI.Controllers
{

    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAppUserService _userService;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAppUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        #region Login

        
        public async Task<IActionResult> Login()
        {
            if (!_userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    Name = "AdminPanel",
                    UserName = "AdminPanel",
                };

                await _userManager.CreateAsync(user, "Admin1234");
            }
            var dto = new AppUserLoginDto();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginDto dto)
        {
            
            if (!_userService.LoginisValid(dto))
            {
                TempData["alerts"] = this.ViewAlert(Common.Enums.AlertType.Info, "Check your values again");
                return View(dto);
            }
            var user = await _userManager.FindByNameAsync(dto.Name);
            
            if (user == null)
            {
                TempData["alerts"] = this.ViewAlert(Common.Enums.AlertType.Warning, "User Can't found");
                return View(dto);
            }

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true, true);

            if (result.Succeeded)
            {
                TempData["alerts"] = this.ViewAlert(Common.Enums.AlertType.Info, "Login Succesfully");
                return RedirectToAction("Index", "Dashboard");
            }

            TempData["alerts"] = this.ViewAlert(Common.Enums.AlertType.Warning, "Check your values again ");

            return View(dto);
        }
        #endregion

        #region userSettings
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(UserSettingsModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Name);

                if (user == null)
                {
                    TempData["alerts"] = this.ViewAlert(Common.Enums.AlertType.Warning, "User Can't found");
                    return RedirectToAction("UserSettings", "Dashboard");
                }
                var result = await _userManager.ChangePasswordAsync(user, model.oldPassword, model.Password);
                if (result.Succeeded)
                {
                    TempData["alerts"] = this.ViewAlert(Common.Enums.AlertType.Succes, "Password changed ");
                    return RedirectToAction("Logout");
                }
                TempData["alerts"] = this.ViewAlert(Common.Enums.AlertType.Info, "Something went wrong");
                return RedirectToAction("UserSettings", "Dashboard");
            }
            TempData["alerts"] = this.ViewAlert(Common.Enums.AlertType.Warning, "something went wrong");
            return RedirectToAction("UserSettings", "Dashboard");
        }
        

        #endregion

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Account/Login");
        }

        #region register 

        //public IActionResult Register()
        //{
        //    var dto = new AppUserRegisterDto();
        //    return View(dto);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(AppUserRegisterDto dto)
        //{
        //    if (!_userService.RegisterisValid(dto))
        //    {
        //        TempData["alerts"] = this.ViewAlert(Common.Enums.AlertType.Info, "Check your values again");
        //        return View(dto);
        //    }

        //    var user = new AppUser()
        //    {
        //        Name = dto.Name.Trim(),
        //        UserName = dto.Name.Trim(),
        //    };

        //    var result = await _userManager.CreateAsync(user, dto.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    TempData["alerts"] = this.ViewAlert(Common.Enums.AlertType.Warning, "Something Went Wrong :(");
        //    return View(dto);

        //}
        #endregion

    }
}
