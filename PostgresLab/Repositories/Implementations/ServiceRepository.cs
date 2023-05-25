using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class ServiceRepository : IServiceRepository
{
    private AcmeDataContext context;

    public ServiceRepository(AcmeDataContext context)
    {
        this.context = context;
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
        Console.WriteLine(context.Database.GetConnectionString());
        var services = context.Services
            .Include(x => x.Master).ToList();

        return services;
    }
}