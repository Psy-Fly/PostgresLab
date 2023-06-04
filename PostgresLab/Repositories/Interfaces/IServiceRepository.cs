namespace PostgresLab.Repositories.Interfaces;

public interface IServiceRepository
{
    Service GetServiceById(int id);
    List<Service> GetServicesList();
    void CreateService(Service service);
}