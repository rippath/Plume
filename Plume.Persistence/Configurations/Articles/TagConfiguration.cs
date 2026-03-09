using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Articles;

namespace Plume.Persistence.Configurations.Articles;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Slug)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.ImageUrl)
            .HasMaxLength(500);

        builder.Property(t => t.ColorHex)
            .HasMaxLength(7);

        // Indexes
        builder.HasIndex(t => t.Name)
            .IsUnique();

        builder.HasIndex(t => t.Slug)
            .IsUnique();

        builder.HasIndex(t => t.IsFeatured);

        builder.HasIndex(t => t.ArticleCount);

        // Self-referencing hierarchy
        builder.HasOne(t => t.ParentTag)
            .WithMany(t => t.ChildTags)
            .HasForeignKey(t => t.ParentTagId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
