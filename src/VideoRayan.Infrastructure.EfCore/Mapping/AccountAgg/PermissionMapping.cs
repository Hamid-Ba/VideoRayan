using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRayan.Domain.AccountAgg;

namespace VideoRayan.Infrastructure.EfCore.Mapping.AccountAgg
{
    internal class PermissionMapping : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasMany(r => r.Roles)
                .WithOne(p => p.Permission)
                .HasForeignKey(f => f.PermissionId);
        }
    }
}