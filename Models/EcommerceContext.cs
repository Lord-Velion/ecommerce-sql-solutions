using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EcommerceSqlSolutions.Models;

public class EcommerceContext : DbContext
{
    public DbSet<Agent> Agents { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Good> Goods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<GoodProperty> GoodProperties { get; set; }

    public string DbPath { get; }

    public EcommerceContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "ecommerce.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasMany(a => a.Orders)
                  .WithOne(o => o.Agent)
                  .HasForeignKey(o => o.AgentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.CreateDate).IsRequired();

            entity.HasMany(o => o.OrderDetails)
                  .WithOne(od => od.Order)
                  .HasForeignKey(od => od.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Good>(entity =>
        {
            entity.HasMany(g => g.OrderDetails)
                  .WithOne(od => od.Good)
                  .HasForeignKey(od => od.GoodId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(g => g.GoodProperties)
                  .WithOne(gp => gp.Good)
                  .HasForeignKey(gp => gp.GoodId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasMany(c => c.GoodProperties)
                  .WithOne(gp => gp.Color)
                  .HasForeignKey(gp => gp.ColorId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<GoodProperty>(entity =>
        {
            entity.Property(gp => gp.Edate).IsRequired(false);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(od => od.GoodCount).IsRequired();
        });

        modelBuilder.Entity<OrderDetail>()
            .ToTable(t => t.HasCheckConstraint("CK_OrderDetail_GoodCount", "GoodCount > 0"));
    }
}