using System;
using System.Collections.Generic;

namespace PostgresLab;

public partial class Organization
{
    public int Id { get; set; }

    public string? OrgAddress { get; set; }

    public string? PostIndex { get; set; }

    public long? PhoneNumber { get; set; }

    public long? WorkersCount { get; set; }

    public string? OrgType { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
