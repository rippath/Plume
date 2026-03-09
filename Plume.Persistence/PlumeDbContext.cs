using Microsoft.EntityFrameworkCore;
using Plume.Domain.Entities.Analytics;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Entities.Social;
using Plume.Domain.Entities.Users;

namespace Plume.Persistence;

public class PlumeDbContext : DbContext
{
    public PlumeDbContext(DbContextOptions<PlumeDbContext> options) : base(options)
    {
    }

    // Users
    public DbSet<User> Users => Set<User>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<UserFollow> UserFollows => Set<UserFollow>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    // Articles
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<ArticleTag> ArticleTags => Set<ArticleTag>();
    public DbSet<ArticleSeries> ArticleSeries => Set<ArticleSeries>();
    public DbSet<ArticleRevision> ArticleRevisions => Set<ArticleRevision>();

    // Social
    public DbSet<Reaction> Reactions => Set<Reaction>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<CommentLike> CommentLikes => Set<CommentLike>();
    public DbSet<Bookmark> Bookmarks => Set<Bookmark>();
    public DbSet<BookmarkFolder> BookmarkFolders => Set<BookmarkFolder>();

    // Analytics
    public DbSet<ArticleView> ArticleViews => Set<ArticleView>();
    public DbSet<ReadingSession> ReadingSessions => Set<ReadingSession>();
    public DbSet<SearchQuery> SearchQueries => Set<SearchQuery>();
    public DbSet<UserTopicInterest> UserTopicInterests => Set<UserTopicInterest>();
    public DbSet<UserAuthorAffinity> UserAuthorAffinities => Set<UserAuthorAffinity>();
    public DbSet<ArticleEngagementStats> ArticleEngagementStats => Set<ArticleEngagementStats>();
    public DbSet<UserArticleInteraction> UserArticleInteractions => Set<UserArticleInteraction>();
    public DbSet<RecommendationEvent> RecommendationEvents => Set<RecommendationEvent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlumeDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Domain.Common.BaseEntity &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (Domain.Common.BaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
            }

            entity.UpdatedDate = DateTime.UtcNow;
        }
    }
}
