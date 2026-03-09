using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Users;

namespace Plume.Persistence.Configurations.Users;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UserProfiles");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Bio)
            .HasMaxLength(2000);

        builder.Property(p => p.Tagline)
            .HasMaxLength(160);

        builder.Property(p => p.AvatarUrl)
            .HasMaxLength(500);

        builder.Property(p => p.CoverImageUrl)
            .HasMaxLength(500);

        builder.Property(p => p.WebsiteUrl)
            .HasMaxLength(255);

        builder.Property(p => p.Location)
            .HasMaxLength(100);

        builder.Property(p => p.TwitterHandle)
            .HasMaxLength(50);

        builder.Property(p => p.LinkedInProfile)
            .HasMaxLength(100);

        builder.Property(p => p.GitHubUsername)
            .HasMaxLength(50);

        // Indexes for denormalized counts (useful for leaderboards/rankings)
        builder.HasIndex(p => p.FollowerCount);
        builder.HasIndex(p => p.TotalArticleViews);
    }
}
