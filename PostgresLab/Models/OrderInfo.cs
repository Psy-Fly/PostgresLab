using System;
using System.Collections.Generic;

namespace PostgresLab;

public partial class OrderInfo
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ServiceId { get; set; }

    public int? Grade { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Service? Service { get; set; }
}
