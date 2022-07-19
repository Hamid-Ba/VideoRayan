using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRayan.Domain.AccountAgg;

namespace VideoRayan.Infrastructure.EfCore.Mapping.AccountAgg
{
    internal class OperatorMapping : IEntityTypeConfiguration<Operator>
    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.FullName).HasMaxLength(125).IsRequired();
            builder.Property(p => p.Mobile).HasMaxLength(11).IsRequired();
            builder.Property(p => p.Password).IsRequired();

            builder.HasOne(r => r.Role)
                .WithMany(o => o.Operators)
                .HasForeignKey(f => f.RoleId);
        }
    }
}