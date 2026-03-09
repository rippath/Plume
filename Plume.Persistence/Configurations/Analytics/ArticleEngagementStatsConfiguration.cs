using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Analytics;

namespace Plume.Persistence.Configurations.Analytics;

public class ArticleEngagementStatsConfiguration : IEntityTypeConfiguration<ArticleEngagementStats>
{
    public void Configure(EntityTypeBuilder<ArticleEngagementStats> builder)
    {
        builder.ToTable("ArticleEngagementStats");

        builder.HasKey(s => s.Id);

        // Precision for decimal fields
        builder.Property(s => s.AvgReadingTimeSeconds)
            .HasPrecision(10, 2);

        builder.Property(s => s.AvgScrollDepth)
            .HasPrecision(5, 4);

        builder.Property(s => s.CompletionRate)
            .HasPrecision(5, 4);

        builder.Property(s => s.BounceRate)
            .HasPrecision(5, 4);

        builder.Property(s => s.EngagementQualityScore)
            .HasPrecision(10, 6);

        builder.Property(s => s.ReactionRate)
            .HasPrecision(10, 6);

        builder.Property(s => s.BookmarkRate)
            .HasPrecision(10, 6);

        builder.Property(s => s.CommentRate)
            .HasPrecision(10, 6);

        builder.Property(s => s.EngagementVelocity)
            .HasPrecision(10, 6);

        builder.Property(s => s.OverallScore)
            .HasPrecision(18, 6);

        // Unique constraint: one stats record per article
        builder.HasIndex(s => s.ArticleId)
            .IsUnique();

        // Index for ranking queries
        builder.HasIndex(s => s.OverallScore);

        // Index for trending (views in last 24h)
        builder.HasIndex(s => s.ViewsLast24Hours);

        // Index for quality content discovery
        builder.HasIndex(s => s.EngagementQualityScore);
    }
}
