using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRayan.Domain.CustomerAgg;

namespace VideoRayan.Infrastructure.EfCore.Mapping.CustomerAgg
{
    internal class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.FirstName).HasMaxLength(50);
            builder.Property(p => p.LastName).HasMaxLength(85);
            builder.Property(p => p.Email).HasMaxLength(200);
            builder.Property(p => p.Title).HasMaxLength(200);
            builder.Property(p => p.Mobile).HasMaxLength(11).IsRequired();

            builder.HasMany(m => m.Meetings).WithOne(c => c.User).HasForeignKey(f => f.UserId);
            builder.HasMany(u => u.Audiences).WithOne(c => c.User).HasForeignKey(f => f.UserId);
        }
    }
}
