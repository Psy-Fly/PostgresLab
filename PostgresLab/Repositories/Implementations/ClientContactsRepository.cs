using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class ClientContactsRepository : IClientContactsRepository
{
    
    private AcmeDataContext context;
    private ConnectionSingleton connectionSingleton;

    public ClientContactsRepository(AcmeDataContext context, ConnectionSingleton connectionSingleton)
    {
        this.context = context;
        this.connectionSingleton = connectionSingleton;
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
    }
    public List<ClientContact> GetClientContactsList()
    {
        try
        {
            var contacts = context.ClientContacts
                .ToList();

            return contacts;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<ClientContact>();
    }

    public EntityEntry<ClientContact> CreateClientContact(ClientContact contact)
    {
        var entry = context.ClientContacts.Add(contact);
        context.SaveChangesAsync();

        return entry;
    }
}