using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Analytics;

namespace Plume.Persistence.Configurations.Analytics;

public class UserAuthorAffinityConfiguration : IEntityTypeConfiguration<UserAuthorAffinity>
{
    public void Configure(EntityTypeBuilder<UserAuthorAffinity> builder)
    {
        builder.ToTable("UserAuthorAffinities");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.AffinityScore)
            .HasPrecision(5, 4);

        builder.Property(a => a.AvgCompletionRate)
            .HasPrecision(5, 4);

        builder.Property(a => a.RecencyWeight)
            .HasPrecision(5, 4)
            .HasDefaultValue(1.0m);

        // Unique constraint: one affinity record per user-author pair
        builder.HasIndex(a => new { a.UserId, a.AuthorId })
            .IsUnique();

        // Index for user's top authors (sorted by affinity)
        builder.HasIndex(a => new { a.UserId, a.AffinityScore });

        // Index for author's fans
        builder.HasIndex(a => a.AuthorId);

        // Index for users who follow an author
        builder.HasIndex(a => new { a.AuthorId, a.IsFollowing });

        builder.HasOne(a => a.User)
            .WithMany(u => u.AuthorAffinities)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Author)
            .WithMany()
            .HasForeignKey(a => a.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
