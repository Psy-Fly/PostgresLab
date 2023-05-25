using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class ClientRepository : IClientRepository
{
    private AcmeDataContext context;

    public ClientRepository(AcmeDataContext context)
    {
        this.context = context;
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
        var clients = context.Clients
            .Include(x => x.Contacts)
            .Include(x => x.Status)
            .ToList();

        return clients;
    }
}