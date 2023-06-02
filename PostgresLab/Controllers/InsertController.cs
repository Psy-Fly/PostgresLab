using Microsoft.AspNetCore.Mvc;
using PostgresLab.Repositories.Interfaces;
using PostgresLab.ViewModels;

namespace PostgresLab.Controllers;

public class InsertController : Controller
{
    private IClientRepository clientRepository;
    private IServiceRepository serviceRepository;
    private IOrderRepository orderRepository;
    private IOrderInfoRepository orderInfoRepository;
    private IClientContactsRepository clientContactsRepository;

    public InsertController(IClientRepository clientRepository, IServiceRepository serviceRepository, IOrderRepository orderRepository, IOrderInfoRepository orderInfoRepository, IClientContactsRepository clientContactsRepository)
    {
        this.clientRepository = clientRepository;
        this.serviceRepository = serviceRepository;
        this.orderRepository = orderRepository;
        this.orderInfoRepository = orderInfoRepository;
        this.clientContactsRepository = clientContactsRepository;
    }

    [HttpGet]
    public IActionResult AddRow()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult AddRow(InsertViewModel model, string form)
    {
        switch (form)
        {
            case "clients":
                AddClient(model.Client);
                break;
            case "services":
                AddService(model.Service);
                break;
            case "orders":
                AddOrder(model.Order);
                break;
            case "order_infos":
                AddOrderInfo(model.OrderInfo);
                break;
        }
        return View(model);
    }

    public void AddClient(Client client)
    {
        var contact = clientContactsRepository.CreateClientContact(client.Contacts);
        client.ContactsId = contact.Property(x => x.Id).CurrentValue;
        clientRepository.CreateClient(client);
    }

    public void AddService(Service service)
    {
        serviceRepository.CreateService(service);
    }
    
    public void AddOrder(Order order)
    {
        orderRepository.CreateOrder(order);
    }
    
    public void AddOrderInfo(OrderInfo info)
    {
        orderInfoRepository.CreateOrderInfo(info);
    }
    
    
}