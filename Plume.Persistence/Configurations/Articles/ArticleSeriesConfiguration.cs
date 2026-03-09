using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Articles;

namespace Plume.Persistence.Configurations.Articles;

public class ArticleSeriesConfiguration : IEntityTypeConfiguration<ArticleSeries>
{
    public void Configure(EntityTypeBuilder<ArticleSeries> builder)
    {
        builder.ToTable("ArticleSeries");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Description)
            .HasMaxLength(1000);

        builder.Property(s => s.CoverImageUrl)
            .HasMaxLength(500);

        // Indexes
        builder.HasIndex(s => s.Slug)
            .IsUnique();

        builder.HasIndex(s => s.AuthorId);

        builder.HasOne(s => s.Author)
            .WithMany()
            .HasForeignKey(s => s.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
