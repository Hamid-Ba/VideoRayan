﻿using Microsoft.EntityFrameworkCore;
using VideoRayan.Domain.AccountAgg;
using VideoRayan.Infrastructure.EfCore.Mapping.AccountAgg;

namespace VideoRayan.Infrastructure.EfCore;
public class VideoRayanContext : DbContext
{
    public VideoRayanContext(DbContextOptions<VideoRayanContext> context) : base(context) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.Restrict;

        var assembly = typeof(OperatorMapping).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        modelBuilder.Entity<Role>().HasQueryFilter(u => !u.IsDelete);
        modelBuilder.Entity<Operator>().HasQueryFilter(u => !u.IsDelete);
    }

    #region AccountAgg

    public DbSet<Operator> Operators { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    #endregion
}