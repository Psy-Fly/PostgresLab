using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class OrderInfoRepository : IOrderInfoRepository
{
    private AcmeDataContext context;

    public OrderInfoRepository(AcmeDataContext context)
    {
        this.context = context;
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
        var oInfo = context.OrderInfos
            .Include(x => x.Order)
            .ThenInclude(x => x.Client)
            .Include(x => x.Service)
            .ThenInclude(x => x.Master)
            .ToList();


        return oInfo;
    }

    public List<OrderInfo> GetOrderInfosByOrderId(int id)
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
}