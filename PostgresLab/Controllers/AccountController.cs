using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PostgresLab.Services.Interfaces;
using PostgresLab.ViewModels;

namespace PostgresLab.Controllers;

public class AccountController : Controller
{
    private IAccountService accountService;
    private ConnectionSingleton connectionSingleton;

    public AccountController(IAccountService accountService, ConnectionSingleton connectionSingleton)
    {
        this.accountService = accountService;
        this.connectionSingleton = connectionSingleton;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await accountService.Login(model);

            if (response.StatusCode == Services.Enums.StatusCode.OK)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));

                return RedirectToAction("Workers", "Table");
            }
            ModelState.AddModelError("",response.Description);
        }

        return View(model);
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
            var response = await accountService.Register(model);

            if (response.StatusCode == Services.Enums.StatusCode.OK)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));

                return RedirectToAction("Workers", "Table");
            }

            ModelState.AddModelError("", response.Description);
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        connectionSingleton.ChangeConnectionUser("postgres", "1501");
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }
}