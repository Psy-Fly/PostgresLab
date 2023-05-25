using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class OrganizationRepository : IOrganizationRepository
{
    private AcmeDataContext context;

    public OrganizationRepository(AcmeDataContext context)
    {
        this.context = context;
    }

    public Organization GetOrganizationById(int id)
    {
        var org = context.Organizations.FirstOrDefault(x => x.Id == id);
        return org;
    }

    public List<Organization> GetOrgList()
    {
        var org = context.Organizations.ToList();
        return org;
    }
}