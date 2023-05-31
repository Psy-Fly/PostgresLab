using System.ComponentModel.DataAnnotations;

namespace PostgresLab.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string? Fullname { get; set; }

    [Required]
    public WorkerContact Contacts { get; set; }

    [Required]
    public int? Experience { get; set; }

    [Required]
    public int? Salary { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    public int? OrganizationId { get; set; }

    [Required] public double? Rating { get; set; } = 0;

    [Required]
    public string? UserLogin { get; set; }
    
    [Required]
    public string? UserPassword { get; set; }
    
    [Required]
    public string? UserRole { get; set; }
}