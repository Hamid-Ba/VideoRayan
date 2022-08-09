using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRayan.Domain.PlanAgg;

namespace VideoRayan.Infrastructure.EfCore.Mapping.PlanAgg
{
    public class PlanMapping : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Title).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500).IsRequired();
            builder.Property(p => p.Ps).HasMaxLength(250);
            builder.Property(p => p.ImageName).HasMaxLength(125);
            builder.Property(p => p.PeriodPerDay).IsRequired();
        }
    }
}