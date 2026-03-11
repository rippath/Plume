using Plume.Domain.Entities.Articles;

namespace Contract.Persistence.Articles;

public interface IArticleRepository
{
    Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> CreateAsync(Article article, CancellationToken cancellationToken = default);
    Task<Article?> UpdateAsync(Article article, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default);
}
