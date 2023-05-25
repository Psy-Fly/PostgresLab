using System.ComponentModel.DataAnnotations;

namespace PostgresLab.ViewModels;

public class LoginViewModel
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}