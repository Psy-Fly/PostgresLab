using System;
using System.Collections.Generic;

namespace PostgresLab;

public partial class Status
{
    public int Id { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
