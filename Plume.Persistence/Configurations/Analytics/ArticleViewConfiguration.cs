using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Analytics;

namespace Plume.Persistence.Configurations.Analytics;

public class ArticleViewConfiguration : IEntityTypeConfiguration<ArticleView>
{
    public void Configure(EntityTypeBuilder<ArticleView> builder)
    {
        builder.ToTable("ArticleViews");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.ReferrerUrl)
            .HasMaxLength(1000);

        builder.Property(v => v.SessionId)
            .HasMaxLength(100);

        builder.Property(v => v.UserAgent)
            .HasMaxLength(500);

        builder.Property(v => v.CountryCode)
            .HasMaxLength(2);

        builder.Property(v => v.DeviceType)
            .HasMaxLength(20);

        // Index for article view counts
        builder.HasIndex(v => v.ArticleId);

        // Index for user's view history
        builder.HasIndex(v => v.UserId);

        // Index for time-based queries
        builder.HasIndex(v => v.ViewedAt);

        // Composite index for analytics (article views over time)
        builder.HasIndex(v => new { v.ArticleId, v.ViewedAt });

        // Index for source attribution analysis
        builder.HasIndex(v => v.Source);

        // Index for geo analytics
        builder.HasIndex(v => v.CountryCode);

        builder.HasOne(v => v.Article)
            .WithMany(a => a.Views)
            .HasForeignKey(v => v.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(v => v.User)
            .WithMany(u => u.ArticleViews)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
