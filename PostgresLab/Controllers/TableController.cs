using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgresLab.Helpers;
using PostgresLab.Repositories;
using PostgresLab.Repositories.Interfaces;
using PostgresLab.ViewModels;

namespace PostgresLab.Controllers;

public class TableController : Controller
{
    private AcmeDataContext context;
    private IWorkerRepository workerRepository;
    private IClientRepository clientRepository;
    private IServiceRepository serviceRepository;
    private IOrganizationRepository organizationRepository;
    private IOrderRepository orderRepository;
    private IOrderInfoRepository orderInfoRepository;
    private readonly IConfiguration configuration;

    public TableController(AcmeDataContext context, IConfiguration configuration, IWorkerRepository workerRepository, IClientRepository clientRepository, IServiceRepository serviceRepository, IOrganizationRepository organizationRepository, IOrderRepository orderRepository, IOrderInfoRepository orderInfoRepository)
    {
        this.context = context;
        this.configuration = configuration;
        this.workerRepository = workerRepository;
        this.clientRepository = clientRepository;
        this.serviceRepository = serviceRepository;
        this.organizationRepository = organizationRepository;
        this.orderRepository = orderRepository;
        this.orderInfoRepository = orderInfoRepository;

    }

    [HttpGet]
    public IActionResult Workers()
    {
        var workers = workerRepository.GetWorkersList();
        return View(workers);
    }
    
    [HttpGet]
    public IActionResult Clients()
    {
        var clients = clientRepository.GetClientsList();
        return View(clients);
    }
    
    [HttpGet]
    public IActionResult Services()
    {
        var services = serviceRepository.GetServicesList();
        return View(services);
    }
    
    [HttpGet]
    public IActionResult Organizations()
    {
        var orgs = organizationRepository.GetOrgList();
        return View(orgs);
    }
    
    [HttpGet]
    public IActionResult Orders()
    {
        var orders = orderRepository.GetOrdersList();
        var orderInfos = orderInfoRepository.GetOrderInfosList();

        List<OrderViewModel> models = new List<OrderViewModel>();
        foreach (var order in orders)
        {
            var orderInfo = orderInfos.FirstOrDefault(x => x.OrderId == order.Id);
            var orderMod = new OrderViewModel()
            {
                OrderId = order.Id,
                ClientId = order.ClientId,
                OrderDate = order.OrderDate,
                Client = order.Client,
                OrderInfo = orderInfo
            };
            models.Add(orderMod);
        }
        return View(models);
    }
    
    [HttpPost]
    public IActionResult OrderInfos(int orderId)
    {
        var infos = orderInfoRepository.GetOrderInfosByOrderId(orderId);
        
        return View(infos);
    }
}