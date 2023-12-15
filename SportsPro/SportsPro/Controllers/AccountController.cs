using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Runtime.InteropServices;

namespace SportsPro.Controllers
{
	public class AccountController : Controller
	{

		private UserManager<User> userManager;
		private SignInManager<User> signInManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new User { UserName = model.UserName, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
				var result = await userManager.CreateAsync(user, model.Password);

				if(result.Succeeded)
				{
					TempData["message"] = $"{user.UserName} was registered successfully";
					await signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty,error.Description);
					}
				}
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult LogIn(string returnURL = "")
		{
			var model = new LoginViewModel { ReturnUrl = returnURL };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> LogIn(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

				if(result.Succeeded)
				{
					if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
					{
						TempData["message"] = $"{model.UserName} successfully logged in";
						return Redirect(model.ReturnUrl);
					}
					else
					{
						TempData["message"] = $"{model.UserName} successfully logged in";
						return RedirectToAction("Index", "Home");
					}
				}
			}
			ModelState.AddModelError("", "Invalid username/password.");
			return View(model);
		}

		public ViewResult AccessDenied()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ChangePassword()
		{
			var model = new ChangePasswordViewModel
			{
				UserName = User.Identity?.Name ?? ""
			};
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await userManager.FindByNameAsync(model.UserName);
				var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

				if (result.Succeeded)
				{
					TempData["message"] = $"Password successfully changed.";
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (IdentityError error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
			return View(model);
		}
	}
}
