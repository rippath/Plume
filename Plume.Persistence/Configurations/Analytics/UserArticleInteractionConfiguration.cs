using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Analytics;

namespace Plume.Persistence.Configurations.Analytics;

public class UserArticleInteractionConfiguration : IEntityTypeConfiguration<UserArticleInteraction>
{
    public void Configure(EntityTypeBuilder<UserArticleInteraction> builder)
    {
        builder.ToTable("UserArticleInteractions");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.MaxScrollDepth)
            .HasPrecision(5, 4);

        builder.Property(i => i.MaxReadingProgress)
            .HasPrecision(5, 4);

        builder.Property(i => i.EngagementScore)
            .HasPrecision(10, 6);

        // Unique constraint: one interaction record per user-article pair
        builder.HasIndex(i => new { i.UserId, i.ArticleId })
            .IsUnique();

        // Index for user's interaction history
        builder.HasIndex(i => new { i.UserId, i.LastViewedAt });

        // Index for article's engaged users
        builder.HasIndex(i => i.ArticleId);

        // Index for positive interactions
        builder.HasIndex(i => new { i.UserId, i.IsPositiveInteraction });

        // Index for completed reads
        builder.HasIndex(i => i.HasCompleted);

        builder.HasOne(i => i.User)
            .WithMany()
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Article)
            .WithMany()
            .HasForeignKey(i => i.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
