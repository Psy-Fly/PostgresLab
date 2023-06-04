using Microsoft.EntityFrameworkCore;
using Npgsql;
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
        context.Database.SetConnectionString(connectionSingleton.GetConnectionString());
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
        try
        {
            var users = context.UserAccounts.Include(x => x.Worker).ToList();
            return users;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        
        return new List<UserAccount>();
        
    }

    public async Task CreateUser(UserAccount user, string nonHashPass)
    {
        var conString = connectionSingleton.GetConnectionString();
        using (var connection = new NpgsqlConnection(conString))
        {
            await connection.OpenAsync();

            var query = $"CREATE USER {user.UserLogin} WITH PASSWORD '{nonHashPass}'";

            using (var command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            
            query = $"GRANT {user.UserRole} to {user.UserLogin}";
            
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
            await connection.CloseAsync();
        }

        await context.UserAccounts.AddAsync(user);
        await context.SaveChangesAsync();
    } 
}