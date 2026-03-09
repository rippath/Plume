using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Analytics;

namespace Plume.Persistence.Configurations.Analytics;

public class ReadingSessionConfiguration : IEntityTypeConfiguration<ReadingSession>
{
    public void Configure(EntityTypeBuilder<ReadingSession> builder)
    {
        builder.ToTable("ReadingSessions");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.MaxScrollDepth)
            .HasPrecision(5, 4);

        builder.Property(s => s.ReadingProgress)
            .HasPrecision(5, 4);

        builder.Property(s => s.SessionId)
            .HasMaxLength(100);

        // Index for article reading stats
        builder.HasIndex(s => s.ArticleId);

        // Index for user's reading history
        builder.HasIndex(s => s.UserId);

        // Index for time-based queries
        builder.HasIndex(s => s.StartedAt);

        // Index for completed sessions
        builder.HasIndex(s => s.IsCompleted);

        // Index for bounce analysis
        builder.HasIndex(s => s.IsBounce);

        // Composite for user-article reading lookup
        builder.HasIndex(s => new { s.UserId, s.ArticleId });

        builder.HasOne(s => s.Article)
            .WithMany(a => a.ReadingSessions)
            .HasForeignKey(s => s.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.User)
            .WithMany(u => u.ReadingSessions)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(s => s.ArticleView)
            .WithMany()
            .HasForeignKey(s => s.ArticleViewId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
