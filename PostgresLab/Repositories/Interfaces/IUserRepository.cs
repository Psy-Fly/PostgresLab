namespace PostgresLab.Repositories.Interfaces;

public interface IUserRepository
{
    UserAccount GetUserByLogin(string login);
    List<UserAccount> GetUsersList();
    Task CreateUser(UserAccount user, string nonHashPass);
}