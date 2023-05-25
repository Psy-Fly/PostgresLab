using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PostgresLab;

public partial class AcmeDataContext : DbContext
{
    public AcmeDataContext(DbContextOptions<AcmeDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientContact> ClientContacts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderInfo> OrderInfos { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    public virtual DbSet<WorkerContact> WorkerContacts { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Server=localhost; Database=smd; User id=postgres; Password=1501; Integrated Security=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactsId).HasColumnName("contacts_id");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Contacts).WithMany(p => p.Clients)
                .HasForeignKey(d => d.ContactsId)
                .HasConstraintName("client_contacts_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Clients)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("client_status_id_fkey");
        });

        modelBuilder.Entity<ClientContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_contacts_pkey");

            entity.ToTable("client_contacts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(100)
                .HasColumnName("home_address");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_pkey");

            entity.ToTable("order");

            entity.HasIndex(e => e.OrderDate, "order_index_date");

            entity.HasIndex(e => e.SumPrice, "order_index_price");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.SumPrice).HasColumnName("sum_price");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("order_client_id_fkey");
        });

        modelBuilder.Entity<OrderInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_info_pkey");

            entity.ToTable("order_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderInfos)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_info_order_id_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.OrderInfos)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("order_info_service_id_fkey");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organization_pkey");

            entity.ToTable("organization");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrgAddress)
                .HasMaxLength(100)
                .HasColumnName("org_address");
            entity.Property(e => e.OrgType)
                .HasMaxLength(50)
                .HasColumnName("org_type");
            entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            entity.Property(e => e.PostIndex)
                .HasMaxLength(6)
                .HasColumnName("post_index");
            entity.Property(e => e.WorkersCount).HasColumnName("workers_count");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_pkey");

            entity.ToTable("service");

            entity.HasIndex(e => e.ServiceName, "service_index_name");

            entity.HasIndex(e => e.Price, "service_index_price");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MasterId).HasColumnName("master_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .HasColumnName("service_name");

            entity.HasOne(d => d.Master).WithMany(p => p.Services)
                .HasForeignKey(d => d.MasterId)
                .HasConstraintName("service_master_id_fkey");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("status");

            entity.HasIndex(e => e.StatusName, "status_index_name");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(30)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("worker_pkey");

            entity.ToTable("worker");

            entity.HasIndex(e => e.WorkerLogin, "pass_unique").IsUnique();

            entity.HasIndex(e => e.WorkerLogin, "worker_index_login");

            entity.HasIndex(e => e.Fullname, "worker_index_name");

            entity.HasIndex(e => e.Rating, "worker_index_rate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.ContactsId).HasColumnName("contacts_id");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.Funciton)
                .HasMaxLength(25)
                .HasColumnName("funciton");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Salary).HasColumnName("salary");
            entity.Property(e => e.WorkerLogin).HasColumnName("worker_login");

            entity.HasOne(d => d.Contacts).WithMany(p => p.Workers)
                .HasForeignKey(d => d.ContactsId)
                .HasConstraintName("worker_contacts_id_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Workers)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("worker_organization_id_fkey");
        });

        modelBuilder.Entity<WorkerContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("worker_contacts_pkey");

            entity.ToTable("worker_contacts");

            entity.HasIndex(e => e.HomeAddress, "worker_index_address");

            entity.HasIndex(e => e.PhoneNumber, "worker_index_number");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(100)
                .HasColumnName("home_address");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .HasColumnName("phone_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
