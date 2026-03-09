using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Analytics;

namespace Plume.Persistence.Configurations.Analytics;

public class RecommendationEventConfiguration : IEntityTypeConfiguration<RecommendationEvent>
{
    public void Configure(EntityTypeBuilder<RecommendationEvent> builder)
    {
        builder.ToTable("RecommendationEvents");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Placement)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Algorithm)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.ModelVersion)
            .HasMaxLength(50);

        builder.Property(e => e.PredictedScore)
            .HasPrecision(10, 6);

        builder.Property(e => e.RecommendationReason)
            .HasMaxLength(200);

        builder.Property(e => e.ExperimentGroup)
            .HasMaxLength(50);

        builder.Property(e => e.FeatureSnapshot)
            .HasColumnType("json");

        // Index for user's recommendation history
        builder.HasIndex(e => new { e.UserId, e.RecommendedAt });

        // Index for article recommendation performance
        builder.HasIndex(e => e.ArticleId);

        // Index for A/B testing analysis
        builder.HasIndex(e => new { e.Algorithm, e.ModelVersion, e.RecommendedAt });

        // Index for experiment analysis
        builder.HasIndex(e => e.ExperimentGroup);

        // Index for click-through rate analysis
        builder.HasIndex(e => new { e.WasDisplayed, e.WasClicked });

        // Index for success rate analysis
        builder.HasIndex(e => e.WasSuccessful);

        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Article)
            .WithMany()
            .HasForeignKey(e => e.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
