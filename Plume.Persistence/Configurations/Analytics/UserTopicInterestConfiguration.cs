using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Analytics;

namespace Plume.Persistence.Configurations.Analytics;

public class UserTopicInterestConfiguration : IEntityTypeConfiguration<UserTopicInterest>
{
    public void Configure(EntityTypeBuilder<UserTopicInterest> builder)
    {
        builder.ToTable("UserTopicInterests");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.InterestScore)
            .HasPrecision(5, 4);

        builder.Property(i => i.Confidence)
            .HasPrecision(5, 4);

        builder.Property(i => i.AvgCompletionRate)
            .HasPrecision(5, 4);

        builder.Property(i => i.RecencyWeight)
            .HasPrecision(5, 4)
            .HasDefaultValue(1.0m);

        // Unique constraint: one interest record per user-tag pair
        builder.HasIndex(i => new { i.UserId, i.TagId })
            .IsUnique();

        // Index for user's top interests (sorted by score)
        builder.HasIndex(i => new { i.UserId, i.InterestScore });

        // Index for tag popularity by interest
        builder.HasIndex(i => i.TagId);

        // Index for explicit interests
        builder.HasIndex(i => i.Source);

        builder.HasOne(i => i.User)
            .WithMany(u => u.TopicInterests)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Tag)
            .WithMany(t => t.UserInterests)
            .HasForeignKey(i => i.TagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
