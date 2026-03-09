using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Users;

namespace Plume.Persistence.Configurations.Users;

public class UserFollowConfiguration : IEntityTypeConfiguration<UserFollow>
{
    public void Configure(EntityTypeBuilder<UserFollow> builder)
    {
        builder.ToTable("UserFollows");

        builder.HasKey(f => f.Id);

        // Unique constraint: user can only follow another user once
        builder.HasIndex(f => new { f.FollowerId, f.FolloweeId })
            .IsUnique();

        // Index for finding followers of a user
        builder.HasIndex(f => f.FolloweeId);

        // Index for finding who a user follows
        builder.HasIndex(f => f.FollowerId);
    }
}
