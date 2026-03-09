using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Articles;

namespace Plume.Persistence.Configurations.Articles;

public class ArticleRevisionConfiguration : IEntityTypeConfiguration<ArticleRevision>
{
    public void Configure(EntityTypeBuilder<ArticleRevision> builder)
    {
        builder.ToTable("ArticleRevisions");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(r => r.Content)
            .IsRequired()
            .HasColumnType("longtext");

        builder.Property(r => r.Summary)
            .HasMaxLength(500);

        builder.Property(r => r.RevisionNote)
            .HasMaxLength(500);

        // Unique revision number per article
        builder.HasIndex(r => new { r.ArticleId, r.RevisionNumber })
            .IsUnique();

        builder.HasOne(r => r.Article)
            .WithMany()
            .HasForeignKey(r => r.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
