using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRayan.Domain.MeetingAgg;

namespace VideoRayan.Infrastructure.EfCore.Mapping.MeetingAgg
{
    internal class MeetingMapping : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title).HasMaxLength(125).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500).IsRequired();

            builder.HasOne(c => c.User).WithMany(m => m.Meetings).HasForeignKey(f => f.UserId);
        }
    }
}