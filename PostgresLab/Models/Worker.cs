using System;
using System.Collections.Generic;

namespace PostgresLab;

public partial class Worker
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string? Funciton { get; set; }

    public int? ContactsId { get; set; }

    public int? Experience { get; set; }

    public int? Salary { get; set; }

    public int Age { get; set; }

    public int? OrganizationId { get; set; }

    public double? Rating { get; set; }

    public string? WorkerLogin { get; set; }

    public virtual WorkerContact? Contacts { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
