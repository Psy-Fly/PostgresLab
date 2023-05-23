using System;
using System.Collections.Generic;

namespace PostgresLab;

public partial class Service
{
    public int Id { get; set; }

    public string? ServiceName { get; set; }

    public int Price { get; set; }

    public int? MasterId { get; set; }

    public virtual Worker? Master { get; set; }

    public virtual ICollection<OrderInfo> OrderInfos { get; set; } = new List<OrderInfo>();
}
