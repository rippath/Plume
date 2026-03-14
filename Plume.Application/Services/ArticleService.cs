using Contract.Persistence.Articles;
using Contract.Services;
using Plume.Application.Mappings;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Enums;

namespace Plume.Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _articleRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> CreateAsync(Article article, CancellationToken cancellationToken = default)
    {
        // Ensure slug uniqueness
        var baseSlug = article.Slug;
        var counter = 1;
        while (await _articleRepository.ExistsBySlugAsync(article.Slug, cancellationToken))
            article.Slug = $"{baseSlug}-{counter++}";

        return await _articleRepository.CreateAsync(article, cancellationToken);
    }

    public async Task<Article?> UpdateAsync(Article article, CancellationToken cancellationToken = default)
    {
        var exists = await _articleRepository.ExistsByIdAsync(article.Id, cancellationToken);
        if (!exists) return null;

        return await _articleRepository.UpdateAsync(article, cancellationToken);
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _articleRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<List<Article>> GetAllByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        return await _articleRepository.GetAllByAuthorIdAsync(authorId, cancellationToken);
    }

    public async Task<bool> ChangeArticleStatusAsync(Guid id, ArticleStatus newStatus, CancellationToken cancellationToken = default)
    {
        return await _articleRepository.ChangeArticleStatusAsync(id, newStatus, cancellationToken);
    }
}
