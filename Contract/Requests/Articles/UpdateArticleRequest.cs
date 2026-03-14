namespace Contract.Requests.Articles;

public class UpdateArticleRequest
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? FeaturedImageUrl { get; set; }
    public string? FeaturedImageAlt { get; set; }
}
