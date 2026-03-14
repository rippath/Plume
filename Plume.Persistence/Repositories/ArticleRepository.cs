using Contract.Persistence.Articles;
using Microsoft.EntityFrameworkCore;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Enums;

namespace Plume.Persistence.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly PlumeDbContext _context;

    public ArticleRepository(PlumeDbContext context)
    {
        _context = context;
    }

    public async Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Articles
            .AsNoTracking()
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<bool> CreateAsync(Article article, CancellationToken cancellationToken = default)
    {
        _context.Articles.Add(article);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<Article?> UpdateAsync(Article article, CancellationToken cancellationToken = default)
    {
        _context.Articles.Update(article);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0 ? article : null;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var article = await _context.Articles
            .AsNoTracking()
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (article is null) return false;

        _context.Articles.Remove(article);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Articles.AnyAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return await _context.Articles.AnyAsync(a => a.Slug == slug, cancellationToken);
    }

    public async Task<List<Article>> GetAllByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        return await _context.Articles
            .AsNoTracking()
            .Where(a => a.AuthorId == authorId && a.Status != ArticleStatus.Deleted)
            .OrderByDescending(a => a.UpdatedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ChangeArticleStatusAsync(Guid id, ArticleStatus newStatus, CancellationToken cancellationToken = default)
    {
        var article = await _context.Articles
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (article is null) return false;

        article.Status = newStatus;

        if (newStatus == ArticleStatus.Published && article.PublishedAt is null)
            article.PublishedAt = DateTime.UtcNow;

        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
