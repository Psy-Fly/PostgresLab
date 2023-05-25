using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private AcmeDataContext context;

    public OrderRepository(AcmeDataContext context)
    {
        this.context = context;
    }

    public Order GetOrderById(int id)
    {
        var ord = context.Orders
            .Include(x => x.Client)
            .Include(x => x.OrderInfos)
            .FirstOrDefault(x => x.Id == id);

        return ord;
    }

    public List<Order> GetOrdersList()
    {
        var ord = context.Orders
            .Include(x => x.Client)
            .Include(x => x.OrderInfos)
            .ToList();

        return ord;
    }
}