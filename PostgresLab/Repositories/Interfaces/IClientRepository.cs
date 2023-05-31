namespace PostgresLab.Repositories.Interfaces;

public interface IClientRepository
{
    Client GetClientById(int id);
    List<Client> GetClientsList();
    void CreateClient(Client client);
}