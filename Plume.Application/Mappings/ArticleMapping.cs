using System.Text.RegularExpressions;
using Contract.Requests.Articles;
using Contract.Responses.Articles;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Enums;

namespace Plume.Application.Mappings;

public static class ArticleMapping
{
    public static Article MapToArticle(this CreateArticleRequest request, Guid authorId)
    {
        return new Article
        {
            AuthorId = authorId,
            Title = request.Title.Trim(),
            Slug = GenerateSlug(request.Title),
            Content = request.Content,
            ContentFormat = ContentFormat.EditorJs,
            Status = request.Publish ? ArticleStatus.Published : ArticleStatus.Draft,
            PublishedAt = request.Publish ? DateTime.UtcNow : null,
            IsRightToLeft = true,
            LanguageCode = "dv",
            FeaturedImageUrl = request.FeaturedImageUrl,
            FeaturedImageAlt = request.FeaturedImageAlt
        };
    }

    public static ArticleResponse MapToResponse(this Article article)
    {
        return new ArticleResponse
        {
            Id = article.Id,
            Title = article.Title,
            Slug = article.Slug,
            Status = article.Status.ToString(),
            Content = article.Content,
            FeaturedImageUrl = article.FeaturedImageUrl,
            PublishedAt = article.PublishedAt,
            UpdatedDate = article.UpdatedDate
        };
    }

    public static Article ApplyUpdate(this UpdateArticleRequest request, Article article)
    {
        article.Title = request.Title.Trim();
        article.Content = request.Content;
        article.FeaturedImageUrl = request.FeaturedImageUrl;
        article.FeaturedImageAlt = request.FeaturedImageAlt;
        return article;
    }

    public static string GenerateSlug(string title)
    {
        var slug = Regex.Replace(title.Trim().ToLowerInvariant(), @"[^\w\s-]", "");
        slug = Regex.Replace(slug, @"\s+", "-");
        slug = Regex.Replace(slug, @"-+", "-").Trim('-');

        if (string.IsNullOrEmpty(slug))
            slug = Guid.NewGuid().ToString("N")[..8];
        else if (slug.Length > 300)
            slug = slug[..300];

        return slug;
    }
}
