using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRayan.Domain;

namespace VideoRayan.Infrastructure.EfCore.Mapping
{
    internal class AudienceMeetingMapping : IEntityTypeConfiguration<AudienceMeeting>
    {
        public void Configure(EntityTypeBuilder<AudienceMeeting> builder)
        {
            builder.HasKey(k => new {k.MeetingId , k.AudienceId});

            builder.HasOne(m => m.Meeting).WithMany(a => a.Audiences).HasForeignKey(f => f.AudienceId);
            builder.HasOne(m => m.Audience).WithMany(a => a.Meetings).HasForeignKey(f => f.MeetingId);
        }
    }
}