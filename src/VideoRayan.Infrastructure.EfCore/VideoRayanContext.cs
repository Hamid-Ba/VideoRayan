using Microsoft.EntityFrameworkCore;
using VideoRayan.Domain;
using VideoRayan.Domain.AccountAgg;
using VideoRayan.Domain.CustomerAgg;
using VideoRayan.Domain.MeetingAgg;
using VideoRayan.Domain.PlanAgg;
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
        modelBuilder.Entity<Meeting>().HasQueryFilter(u => !u.IsDelete);
        modelBuilder.Entity<Operator>().HasQueryFilter(u => !u.IsDelete);
        modelBuilder.Entity<Category>().HasQueryFilter(u => !u.IsDelete);
        modelBuilder.Entity<Customer>().HasQueryFilter(u => !u.IsDelete);
        modelBuilder.Entity<Audience>().HasQueryFilter(u => !u.IsDelete);
        modelBuilder.Entity<FaceToFace>().HasQueryFilter(u => !u.IsDelete);
    }

    #region AccountAgg

    public DbSet<Operator> Operators { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    #endregion

    #region CustomerAgg

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Audience> Audiences { get; set; }

    #endregion

    #region Plan

    public DbSet<Plan> Plans { get; set; }

    #endregion

    #region MeetingAgg

    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<FaceToFace> FaceToFaces { get; set; }
    public DbSet<Category> Categories { get; set; }

    #endregion

    public DbSet<AudienceMeeting> AudienceMeetings { get; set; }
    public DbSet<AudienceFaceToFace> AudienceFaceToFaces { get; set; }
}