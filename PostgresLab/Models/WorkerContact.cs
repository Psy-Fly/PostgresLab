using System;
using System.Collections.Generic;

namespace PostgresLab;

public partial class WorkerContact
{
    public int Id { get; set; }

    public string? PhoneNumber { get; set; }

    public string? HomeAddress { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
