using System.Security.Cryptography;
using System.Text;

namespace PostgresLab.Helpers;

public class HashPasswordHelper
{
    public string HashPassword(string password)
    {
        using (var sha = SHA256.Create())
        {
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            var hash = BitConverter.ToString(bytes).Replace("-", "");

            return hash;
        }
    }
}