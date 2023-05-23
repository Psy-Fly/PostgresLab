using Microsoft.AspNetCore.Mvc;

namespace PostgresLab.Controllers;

public class TableController : Controller
{
    private AcmeDataContext context;

    public TableController(AcmeDataContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IActionResult Workers()
    {
        var workers = context.Workers.ToList();
        
        return View(workers);
    }
    
    [HttpGet]
    public IActionResult Clients()
    {
        var clients = context.Clients.ToList();
        return View(clients);
    }
    
    [HttpGet]
    public IActionResult Services()
    {
        var services = context.Services.ToList();
        return View(services);
    }
}