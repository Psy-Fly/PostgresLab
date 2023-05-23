using System;
using System.Collections.Generic;

namespace PostgresLab;

public partial class Order
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public double? SumPrice { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();
}
