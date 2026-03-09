using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Social;

namespace Plume.Persistence.Configurations.Social;

public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
{
    public void Configure(EntityTypeBuilder<Bookmark> builder)
    {
        builder.ToTable("Bookmarks");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Note)
            .HasMaxLength(500);

        // Unique constraint: user can bookmark an article only once
        builder.HasIndex(b => new { b.UserId, b.ArticleId })
            .IsUnique();

        // Index for loading user's bookmarks
        builder.HasIndex(b => new { b.UserId, b.CreatedDate })
            .IsDescending(false, true);

        // Index for bookmarks in a folder
        builder.HasIndex(b => b.FolderId);

        builder.HasOne(b => b.User)
            .WithMany(u => u.Bookmarks)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Article)
            .WithMany(a => a.Bookmarks)
            .HasForeignKey(b => b.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(b => b.Folder)
            .WithMany(f => f.Bookmarks)
            .HasForeignKey(b => b.FolderId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
