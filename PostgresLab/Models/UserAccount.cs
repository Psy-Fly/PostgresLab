using System;
using System.Collections.Generic;

namespace PostgresLab;

public partial class UserAccount
{
    public int Id { get; set; }

    public string? UserLogin { get; set; }

    public string? UserRole { get; set; }

    public string? UserPassword { get; set; }

    public int? WorkerId { get; set; }

    public virtual Worker? Worker { get; set; }
}
