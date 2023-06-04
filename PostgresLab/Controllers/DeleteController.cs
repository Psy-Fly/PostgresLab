using Microsoft.AspNetCore.Mvc;
using PostgresLab.Repositories.Interfaces;
using PostgresLab.ViewModels;

namespace PostgresLab.Controllers;

public class DeleteController : Controller
{
    private IClientRepository clientRepository;
    private IServiceRepository serviceRepository;
    private IOrderRepository orderRepository;
    private IOrderInfoRepository orderInfoRepository;
    private IClientContactsRepository clientContactsRepository;

    public DeleteController(IClientRepository clientRepository, IServiceRepository serviceRepository, IOrderRepository orderRepository, IOrderInfoRepository orderInfoRepository, IClientContactsRepository clientContactsRepository)
    {
        this.clientRepository = clientRepository;
        this.serviceRepository = serviceRepository;
        this.orderRepository = orderRepository;
        this.orderInfoRepository = orderInfoRepository;
        this.clientContactsRepository = clientContactsRepository;
    }
    

    public IActionResult DeleteClientById(int clientId)
    {
        var orders = orderRepository.GetOrdersList().Where(x => x.ClientId == clientId).ToList();
        var orderInfos = orderInfoRepository.GetOrderInfosList().Where(x => orders.Contains(x.Order)).ToList();

        orderInfoRepository.DeleteOrderInfosRange(orderInfos);
        orderRepository.DeleteOrdersRange(orders);

        var clientContactsId = clientRepository.GetClientById(clientId).ContactsId;
        clientContactsRepository.DeleteContactById(Convert.ToInt32(clientContactsId));
        clientRepository.DeleteClientById(clientId);
        return RedirectToAction("CLients", "Table");
    }

    public IActionResult DeleteOrder(int orderId)
    {
        var orderInfos = orderInfoRepository.GetOrderInfosByOrderId(orderId);
        orderInfoRepository.DeleteOrderInfosRange(orderInfos);
        orderRepository.DeleteOrderById(orderId);
        return RedirectToAction("Orders", "Table");
    }
    
    public IActionResult DeleteOrderInfo(int orderInfoId)
    {
        orderInfoRepository.DeleteOrderInfoById(orderInfoId);
        return RedirectToAction("Orders", "Table");
    }
}