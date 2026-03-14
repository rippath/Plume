using Plume.Domain.Entities.Articles;
using Plume.Domain.Enums;

namespace Contract.Services;

public interface IArticleService
{
    Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> CreateAsync(Article article, CancellationToken cancellationToken = default);
    Task<Article?> UpdateAsync(Article article, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Article>> GetAllByAuthorIdAsync(Guid authorId, CancellationToken cancellationToken = default);
    Task<bool> ChangeArticleStatusAsync(Guid id, ArticleStatus newStatus, CancellationToken cancellationToken = default);
}
