using Microsoft.EntityFrameworkCore;
using PostgresLab.Repositories.Interfaces;

namespace PostgresLab.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private AcmeDataContext context;

    public UserRepository(AcmeDataContext context)
    {
        this.context = context;
    }

    public UserAccount GetUserByLogin(string login)
    {
        var user = context.UserAccounts
            .Include(x => x.Worker)
            .FirstOrDefault(x => x.UserLogin == login);

        return user;
    }

    public List<UserAccount> GetUsersList()
    {
        throw new NotImplementedException();
    }

    public async Task CreateUser(UserAccount user)
    {
        await context.UserAccounts.AddAsync(user);
        await context.Database.ExecuteSqlAsync($"create user {user.UserLogin} with password '{user.UserPassword}'");
    } 
}