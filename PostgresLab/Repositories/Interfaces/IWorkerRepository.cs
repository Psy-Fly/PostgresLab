namespace PostgresLab.Repositories.Interfaces;

public interface IWorkerRepository
{
    Worker GetWorkerByLogin(string login);
    List<Worker> GetWorkersList();
    
}