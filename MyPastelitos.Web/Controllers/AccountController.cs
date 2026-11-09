using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.DTOs.Account;
using MyPastelitos.Web.Services.Abstractions;
using MySignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MyPastelitos.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly INotyfService _notyfService;

        public AccountController(IAccountService usersService, INotyfService notyfService)
        {
            _accountService = usersService;
            _notyfService = notyfService;
        }

        [HttpGet]

        public IActionResult login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]  

        public async Task<IActionResult> login(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                Response<MySignInResult> result = await _accountService.LoginAsync(dto);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
                _notyfService.Error("Email o contraseña incorrectos");
            }
            else
            {
                _notyfService.Error("Debe ajustar los errores de validación");

            }

            return View(dto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogOutAsync();
            return RedirectToAction(nameof(login));
        }
    }
}
