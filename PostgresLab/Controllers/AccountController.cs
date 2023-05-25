using Microsoft.AspNetCore.Mvc;
using PostgresLab.ViewModels;

namespace PostgresLab.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        
        return RedirectToAction("Workers", "Table");
    }
}