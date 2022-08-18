using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoRayan.Domain;

namespace VideoRayan.Infrastructure.EfCore.Mapping
{
    public class AudienceFaceToFaceMapping : IEntityTypeConfiguration<AudienceFaceToFace>
    {
        public void Configure(EntityTypeBuilder<AudienceFaceToFace> builder)
        {
            builder.HasKey(k => new { k.FaceToFaceId, k.AudienceId });

            builder.HasOne(m => m.FaceToFace).WithMany(a => a.Audiences).HasForeignKey(f => f.FaceToFaceId);
            builder.HasOne(m => m.Audience).WithMany(a => a.FaceToFaces).HasForeignKey(f => f.AudienceId);
        }
    }
}