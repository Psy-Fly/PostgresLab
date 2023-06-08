using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using PostgresLab.Services;
using PostgresLab.Services.Enums;

namespace PostgresLab.Controllers;

public class ReportController : Controller
{
    private ConnectionSingleton connectionSingleton;

    public ReportController(ConnectionSingleton connectionSingleton)
    {
        this.connectionSingleton = connectionSingleton;
    }

    [Authorize(Roles = "admin")]
    public IActionResult ClientReport()
    {
        var conString = connectionSingleton.GetConnectionString();
        using (var connection = new NpgsqlConnection(conString))
        {
            connection.Open();
            if(!Directory.Exists(@"C:\Users\Public\reports\"))
                Directory.CreateDirectory("C:\\Users\\Public\\reports\\");
            
            var cmd = $"select copy_to_csv('client', 'clients', 'C:\\Users\\Public\\reports\\')";

            using (var command = new NpgsqlCommand(cmd, connection))
            {
                var x = command.ExecuteNonQuery();
            }
        }
        return RedirectToAction("Workers", "Table");
    }

    [Authorize(Roles = "admin")]
    public IActionResult OrderReport()
    {
        var conString = connectionSingleton.GetConnectionString();
        using (var connection = new NpgsqlConnection(conString))
        {
            connection.Open();
            if(!Directory.Exists(@"C:\Users\Public\reports\"))
                Directory.CreateDirectory("C:\\Users\\Public\\reports\\");
            
            var cmd = $"select copy_to_csv('\"order\"', 'orders', 'C:\\Users\\Public\\reports\\')";

            using (var command = new NpgsqlCommand(cmd, connection))
            {
                var x = command.ExecuteNonQuery();
            }
        }
        return RedirectToAction("Workers", "Table");
    }
}