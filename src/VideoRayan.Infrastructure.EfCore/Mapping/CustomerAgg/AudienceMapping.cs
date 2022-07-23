using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRayan.Domain.CustomerAgg;

namespace VideoRayan.Infrastructure.EfCore.Mapping.CustomerAgg
{
    internal class AudienceMapping : IEntityTypeConfiguration<Audience>
    {
        public void Configure(EntityTypeBuilder<Audience> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.FullName).HasMaxLength(125).IsRequired();
            builder.Property(p => p.Mobile).HasMaxLength(11).IsRequired();
            builder.Property(p => p.Position).HasMaxLength(125).IsRequired();

            builder.HasOne(c => c.User).WithMany(a => a.Audiences).HasForeignKey(f => f.UserId);
            builder.HasOne(c => c.Category).WithMany(a => a.Audiences).HasForeignKey(f => f.CategoryId);
        }
    }
}