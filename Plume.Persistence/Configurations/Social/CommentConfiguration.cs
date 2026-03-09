using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Social;

namespace Plume.Persistence.Configurations.Social;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(c => c.HideReason)
            .HasMaxLength(500);

        // Index for loading article's comments
        builder.HasIndex(c => new { c.ArticleId, c.CreatedDate });

        // Index for loading replies to a comment
        builder.HasIndex(c => c.ParentCommentId);

        // Index for user's comment history
        builder.HasIndex(c => c.UserId);

        // Index for filtering hidden comments
        builder.HasIndex(c => c.IsHidden);

        builder.HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Article)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Self-referencing for threading
        builder.HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
