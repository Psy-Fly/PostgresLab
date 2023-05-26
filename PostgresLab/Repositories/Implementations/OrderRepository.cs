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
    }

    public Order GetOrderById(int id)
    {
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
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
            context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
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
}