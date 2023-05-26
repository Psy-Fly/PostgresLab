using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private AcmeDataContext context;
    private ConnectionSingleton connectionSingleton;

    public UserRepository(AcmeDataContext context, ConnectionSingleton connectionSingleton)
    {
        this.context = context;
        this.connectionSingleton = connectionSingleton;
    }

    public UserAccount GetUserByLogin(string login)
    {
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
        var user = context.UserAccounts
            .Include(x => x.Worker)
            .FirstOrDefault(x => x.UserLogin == login);

        return user;
    }

    public List<UserAccount> GetUsersList()
    {
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
        throw new NotImplementedException();
    }

    public async Task CreateUser(UserAccount user)
    {
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
        await context.UserAccounts.AddAsync(user);
        await context.Database.ExecuteSqlAsync($"create user {user.UserLogin} with password '{user.UserPassword}'");
    } 
}