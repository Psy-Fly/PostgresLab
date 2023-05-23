using System;
using System.Collections.Generic;

namespace PostgresLab;

public partial class ClientContact
{
    public int Id { get; set; }

    public string? PhoneNumber { get; set; }

    public string? HomeAddress { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
