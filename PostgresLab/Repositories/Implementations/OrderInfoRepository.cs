using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class OrderInfoRepository : IOrderInfoRepository
{
    private AcmeDataContext context;
    private ConnectionSingleton connectionSingleton;

    public OrderInfoRepository(AcmeDataContext context, ConnectionSingleton connectionSingleton)
    {
        this.context = context;
        this.connectionSingleton = connectionSingleton;
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
    }

    public OrderInfo GetOrderInfoById(int id)
    {
        var oInfo = context.OrderInfos
            .Include(x => x.Order)
            .ThenInclude(x => x.Client)
            .Include(x => x.Service)
            .ThenInclude(x => x.Master)
            .FirstOrDefault(x => x.Id == id);

        return oInfo;
    }

    public List<OrderInfo> GetOrderInfosList()
    {
        try
        {
            var oInfo = context.OrderInfos
                .Include(x => x.Order)
                .ThenInclude(x => x.Client)
                .Include(x => x.Service)
                .ThenInclude(x => x.Master)
                .ToList();

            return oInfo;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<OrderInfo>();
    }

    public List<OrderInfo> GetOrderInfosByOrderId(int id)
    {
        try
        {
            var oInfo = context.OrderInfos
                .Include(x => x.Order)
                .ThenInclude(x => x.Client)
                .Include(x => x.Service)
                .ThenInclude(x => x.Master)
                .Where(x => x.OrderId == id)
                .ToList();

            return oInfo;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<OrderInfo>();
    }

    public void CreateOrderInfo(OrderInfo orderInfo)
    {
        context.OrderInfos.Add(orderInfo);
        context.SaveChangesAsync();
    }
    
    public  void DeleteOrderInfoById(int id)
    {
        var orderInfo = context.OrderInfos.Find(id);
        context.OrderInfos.Remove(orderInfo);
        context.SaveChanges();
    }


    public void DeleteOrderInfosRange(List<OrderInfo> infos)
    {
        context.OrderInfos.RemoveRange(infos);
        context.SaveChanges();
    }
}