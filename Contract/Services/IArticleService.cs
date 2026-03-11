using Plume.Domain.Entities.Articles;

namespace Contract.Services;

public interface IArticleService
{
    Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> CreateAsync(Article article, CancellationToken cancellationToken = default);
    Task<Article?> UpdateAsync(Article article, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
