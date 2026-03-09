using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Social;

namespace Plume.Persistence.Configurations.Social;

public class CommentLikeConfiguration : IEntityTypeConfiguration<CommentLike>
{
    public void Configure(EntityTypeBuilder<CommentLike> builder)
    {
        builder.ToTable("CommentLikes");

        builder.HasKey(cl => cl.Id);

        // Unique constraint: user can like a comment only once
        builder.HasIndex(cl => new { cl.UserId, cl.CommentId })
            .IsUnique();

        // Index for counting likes on a comment
        builder.HasIndex(cl => cl.CommentId);

        builder.HasOne(cl => cl.User)
            .WithMany()
            .HasForeignKey(cl => cl.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cl => cl.Comment)
            .WithMany(c => c.Likes)
            .HasForeignKey(cl => cl.CommentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
