namespace PostgresLab;

public class ConnectionSingleton
{
    private string ConnectionString = "Server = localhost; Port=5432; Database=smd; User Id=postgres; Password = 1501";

    public string GetConnectionString()
    {
        return ConnectionString;
    }

    public void ChangeConnectionUser(string userName, string pass)
    {
        ConnectionString = $"Server = localhost; Port=5432; Database=smd; User Id={userName}; Password = {pass}";
    }
}