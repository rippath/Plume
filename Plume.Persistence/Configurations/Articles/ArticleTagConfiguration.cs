using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Articles;

namespace Plume.Persistence.Configurations.Articles;

public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
{
    public void Configure(EntityTypeBuilder<ArticleTag> builder)
    {
        builder.ToTable("ArticleTags");

        builder.HasKey(at => at.Id);

        // Unique constraint: article can have each tag only once
        builder.HasIndex(at => new { at.ArticleId, at.TagId })
            .IsUnique();

        // Index for finding articles by tag
        builder.HasIndex(at => at.TagId);

        // Index for ordering tags within an article
        builder.HasIndex(at => new { at.ArticleId, at.Order });

        builder.HasOne(at => at.Article)
            .WithMany(a => a.ArticleTags)
            .HasForeignKey(at => at.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(at => at.Tag)
            .WithMany(t => t.ArticleTags)
            .HasForeignKey(at => at.TagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
