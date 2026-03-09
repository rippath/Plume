using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Articles;

namespace Plume.Persistence.Configurations.Articles;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Articles");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(a => a.Slug)
            .IsRequired()
            .HasMaxLength(350);

        builder.Property(a => a.Summary)
            .HasMaxLength(500);

        builder.Property(a => a.Content)
            .IsRequired()
            .HasColumnType("longtext");

        builder.Property(a => a.FeaturedImageUrl)
            .HasMaxLength(500);

        builder.Property(a => a.FeaturedImageAlt)
            .HasMaxLength(300);

        builder.Property(a => a.CanonicalUrl)
            .HasMaxLength(500);

        builder.Property(a => a.LanguageCode)
            .HasMaxLength(10)
            .HasDefaultValue("dv");

        builder.Property(a => a.EngagementScore)
            .HasPrecision(18, 6);

        // Indexes
        builder.HasIndex(a => a.Slug)
            .IsUnique();

        builder.HasIndex(a => a.AuthorId);

        builder.HasIndex(a => a.Status);

        builder.HasIndex(a => a.PublishedAt);

        builder.HasIndex(a => a.IsFeatured);

        // Composite index for feed queries
        builder.HasIndex(a => new { a.Status, a.PublishedAt });

        // Index for engagement-based sorting
        builder.HasIndex(a => a.EngagementScore);

        // Relationships
        builder.HasOne(a => a.Author)
            .WithMany(u => u.Articles)
            .HasForeignKey(a => a.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Series)
            .WithMany(s => s.Articles)
            .HasForeignKey(a => a.SeriesId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.EngagementStats)
            .WithOne(s => s.Article)
            .HasForeignKey<Domain.Entities.Analytics.ArticleEngagementStats>(s => s.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
