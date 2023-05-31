using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class ClientRepository : IClientRepository
{
    private AcmeDataContext context;
    private ConnectionSingleton connectionSingleton;

    public ClientRepository(AcmeDataContext context, ConnectionSingleton connectionSingleton)
    {
        this.context = context;
        this.connectionSingleton = connectionSingleton;
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
    }

    public Client GetClientById(int id)
    {
        var client = context.Clients
            .Include(x => x.Contacts)
            .Include(x => x.Status)
            .FirstOrDefault(x => x.Id == id);

        return client;
    }

    public List<Client> GetClientsList()
    {
        try
        {
            var clients = context.Clients
                .Include(x => x.Contacts)
                .Include(x => x.Status)
                .ToList();

            return clients;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<Client>();

    }

    public void CreateClient(Client client)
    {
        context.Clients.Add(client);
        context.SaveChangesAsync();
    }
}