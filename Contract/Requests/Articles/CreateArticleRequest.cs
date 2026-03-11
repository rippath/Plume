namespace Contract.Requests.Articles;

public class CreateArticleRequest
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool Publish { get; set; }
}
