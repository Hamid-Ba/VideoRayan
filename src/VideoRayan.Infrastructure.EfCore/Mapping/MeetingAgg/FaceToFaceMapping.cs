using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRayan.Domain.MeetingAgg;

namespace VideoRayan.Infrastructure.EfCore.Mapping.MeetingAgg
{
    public class FaceToFaceMapping : IEntityTypeConfiguration<FaceToFace>
    {
        public void Configure(EntityTypeBuilder<FaceToFace> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title).HasMaxLength(125).IsRequired();

            builder.HasOne(c => c.User).WithMany(m => m.FaceToFaces).HasForeignKey(f => f.UserId);
        }
    }
}