using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class WorkerRepository : IWorkerRepository
{
    private AcmeDataContext context;

    public WorkerRepository(AcmeDataContext context)
    {
        this.context = context;
    }

    public Worker GetWorkerByLogin(string login)
    {
        var worker = context.Workers.Include(x => x.Contacts)
            .Include(x => x.Organization)
            .FirstOrDefault(x => x.WorkerLogin == login);

        return worker;
    }

    public List<Worker> GetWorkersList()
    {
        var workers = context.Workers.Include(x => x.Contacts).ToList();
        
        return workers;
    }

    public async Task CreateWorker(Worker worker)
    {
         await context.Workers.AddAsync(worker);
    }
}