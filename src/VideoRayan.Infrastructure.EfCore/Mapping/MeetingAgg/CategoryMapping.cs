using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRayan.Domain.MeetingAgg;

namespace VideoRayan.Infrastructure.EfCore.Mapping.MeetingAgg
{
    internal class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Title).HasMaxLength(55).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(250).IsRequired();

            builder.HasMany(a => a.Audiences).WithOne(c => c.Category).HasForeignKey(f => f.CategoryId);
            builder.HasOne(u => u.Customer).WithMany(c => c.Categories).HasForeignKey(f => f.CustomerId);
        }
    }
}