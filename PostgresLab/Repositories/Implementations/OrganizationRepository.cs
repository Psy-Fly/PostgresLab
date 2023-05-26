using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class OrganizationRepository : IOrganizationRepository
{
    private AcmeDataContext context;
    private ConnectionSingleton connectionSingleton;

    public OrganizationRepository(AcmeDataContext context, ConnectionSingleton connectionSingleton)
    {
        this.context = context;
        this.connectionSingleton = connectionSingleton;
    }

    public Organization GetOrganizationById(int id)
    {
        try
        {
            context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
            var org = context.Organizations.FirstOrDefault(x => x.Id == id);
            return org;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new Organization();
    }

    public List<Organization> GetOrgList()
    {
        try
        {
            context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
            var org = context.Organizations.ToList();
            return org;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<Organization>();
    }
}