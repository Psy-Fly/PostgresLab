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
    }

    public Service GetServiceById(int id)
    {
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
        var service = context.Services
            .Include(x => x.Master)
            .FirstOrDefault(x => x.Id == id);

        return service;
    }

    public List<Service> GetServicesList()
    {
        try
        {
            context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
            Console.WriteLine(context.Database.GetConnectionString());
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
}