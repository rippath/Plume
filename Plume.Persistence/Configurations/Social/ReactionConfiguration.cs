using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Social;

namespace Plume.Persistence.Configurations.Social;

public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
{
    public void Configure(EntityTypeBuilder<Reaction> builder)
    {
        builder.ToTable("Reactions");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Count)
            .HasDefaultValue(1);

        // Unique constraint: one reaction type per user per article
        builder.HasIndex(r => new { r.UserId, r.ArticleId, r.Type })
            .IsUnique();

        // Index for counting reactions on an article
        builder.HasIndex(r => r.ArticleId);

        // Index for finding user's reactions
        builder.HasIndex(r => r.UserId);

        builder.HasOne(r => r.User)
            .WithMany(u => u.Reactions)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Article)
            .WithMany(a => a.Reactions)
            .HasForeignKey(r => r.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
