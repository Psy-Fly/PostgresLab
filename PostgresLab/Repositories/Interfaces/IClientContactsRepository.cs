using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PostgresLab.Repositories.Interfaces;

public interface IClientContactsRepository
{
    List<ClientContact> GetClientContactsList();
    EntityEntry<ClientContact> CreateClientContact(ClientContact contact);
}