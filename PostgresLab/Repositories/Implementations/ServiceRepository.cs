using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class ServiceRepository : IServiceRepository
{
    private AcmeDataContext context;
    private ConnectionSingleton connectionSingleton;

    public ServiceRepository(AcmeDataContext context, ConnectionSingleton connectionSingleton)
    {
        this.context = context;
        this.connectionSingleton = connectionSingleton;
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
    }

    public Service GetServiceById(int id)
    {

        var service = context.Services
            .Include(x => x.Master)
            .FirstOrDefault(x => x.Id == id);

        return service;
    }

    public List<Service> GetServicesList()
    {
        try
        {
            var services = context.Services
                .Include(x => x.Master).ToList();
            return services;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<Service>();
    }

    public void CreateService(Service service)
    {
        context.Services.Add(service);
        context.SaveChangesAsync();
    }
}