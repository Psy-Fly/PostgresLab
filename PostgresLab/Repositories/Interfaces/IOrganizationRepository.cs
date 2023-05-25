namespace PostgresLab.Repositories.Interfaces;

public interface IOrganizationRepository
{
    Organization GetOrganizationById(int id);
    List<Organization> GetOrgList();
}