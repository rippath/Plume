using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Social;

namespace Plume.Persistence.Configurations.Social;

public class BookmarkFolderConfiguration : IEntityTypeConfiguration<BookmarkFolder>
{
    public void Configure(EntityTypeBuilder<BookmarkFolder> builder)
    {
        builder.ToTable("BookmarkFolders");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.Description)
            .HasMaxLength(500);

        // Index for loading user's folders
        builder.HasIndex(f => new { f.UserId, f.Order });

        // Unique folder name per user
        builder.HasIndex(f => new { f.UserId, f.Name })
            .IsUnique();

        builder.HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
