namespace PostgresLab.Repositories.Interfaces;

public interface IOrderRepository
{
    Order GetOrderById(int id);

    List<Order> GetOrdersList();
}