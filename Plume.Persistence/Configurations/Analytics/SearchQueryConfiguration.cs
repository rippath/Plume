using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plume.Domain.Entities.Analytics;

namespace Plume.Persistence.Configurations.Analytics;

public class SearchQueryConfiguration : IEntityTypeConfiguration<SearchQuery>
{
    public void Configure(EntityTypeBuilder<SearchQuery> builder)
    {
        builder.ToTable("SearchQueries");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Query)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(q => q.NormalizedQuery)
            .HasMaxLength(500);

        builder.Property(q => q.FiltersApplied)
            .HasMaxLength(1000);

        builder.Property(q => q.SessionId)
            .HasMaxLength(100);

        // Index for user's search history
        builder.HasIndex(q => q.UserId);

        // Index for search analytics
        builder.HasIndex(q => q.SearchedAt);

        // Index for popular queries analysis
        builder.HasIndex(q => q.NormalizedQuery);

        // Index for click-through analysis
        builder.HasIndex(q => q.HasClick);

        builder.HasOne(q => q.User)
            .WithMany(u => u.SearchQueries)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
