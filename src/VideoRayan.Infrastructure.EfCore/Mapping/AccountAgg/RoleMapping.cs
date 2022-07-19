using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRayan.Domain.AccountAgg;

namespace VideoRayan.Infrastructure.EfCore.Mapping.AccountAgg
{
    internal class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();

            builder.HasMany(o => o.Operators)
                .WithOne(r => r.Role)
                .HasForeignKey(f => f.RoleId);

            builder.HasMany(p => p.Permissions)
                .WithOne(r => r.Role)
                .HasForeignKey(f => f.RoleId);
        }
    }
}