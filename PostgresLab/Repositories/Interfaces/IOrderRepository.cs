namespace PostgresLab.Repositories.Interfaces;

public interface IOrderRepository
{
    Order GetOrderById(int id);

    List<Order> GetOrdersList();

    void CreateOrder(Order order);
    void DeleteOrderById(int id);
    void DeleteOrdersRange(List<Order> orders);
}