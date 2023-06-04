using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private AcmeDataContext context;
    private ConnectionSingleton connectionSingleton;

    public OrderRepository(AcmeDataContext context, ConnectionSingleton connectionSingleton)
    {
        this.context = context;
        this.connectionSingleton = connectionSingleton;
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
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
        try
        {
            var ord = context.Orders
                .Include(x => x.Client)
                .Include(x => x.OrderInfos)
                .ToList();

            return ord;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<Order>();
    }

    public void CreateOrder(Order order)
    {
        context.Orders.Add(order);
        context.SaveChangesAsync();
    }

    public void DeleteOrderById(int id)
    {
        var order = context.Orders.Find(id);
        context.Orders.Remove(order);
        context.SaveChanges();
    }

    public void DeleteOrdersRange(List<Order> orders)
    {
        context.RemoveRange(orders);
        context.SaveChanges();
    }
}