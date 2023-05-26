using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class WorkerRepository : IWorkerRepository
{
    private AcmeDataContext context;
    private ConnectionSingleton connectionSingleton;

    public WorkerRepository(AcmeDataContext context, ConnectionSingleton connectionSingleton)
    {
        this.context = context;
        this.connectionSingleton = connectionSingleton;
    }

    public Worker GetWorkerByLogin(string login)
    {

        try
        {
            context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
            var worker = context.Workers.Include(x => x.Contacts)
                .Include(x => x.Organization)
                .FirstOrDefault(x => x.WorkerLogin == login);
            return worker;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new Worker();
    }

    public List<Worker> GetWorkersList()
    {
        try
        {
            context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
            var workers = context.Workers.Include(x => x.Contacts).ToList();
            return workers;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return new List<Worker>();
    }

    public async Task CreateWorker(Worker worker)
    {
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
        await context.Workers.AddAsync(worker);
    }
}