using System;
using System.Collections.Generic;

namespace PostgresLab;

public partial class Client 
{
    public int Id { get; set; }

    public int? StatusId { get; set; }

    public int? ContactsId { get; set; }

    public string? Fullname { get; set; }

    public virtual ClientContact? Contacts { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Status? Status { get; set; }
}
