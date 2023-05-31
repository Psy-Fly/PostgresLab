namespace PostgresLab.Repositories.Interfaces;

public interface IOrderInfoRepository
{

    OrderInfo GetOrderInfoById(int id);

    List<OrderInfo> GetOrderInfosList();
    List<OrderInfo> GetOrderInfosByOrderId(int id);

    void CreateOrderInfo(OrderInfo orderInfo);
}