using System.Security.Claims;
using Contract.Common;
using Contract.Requests.Articles;
using Contract.Responses.Articles;
using Contract.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plume.Application.Mappings;

namespace Plume.UI.Controllers;

[Authorize]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpPost(EndPoints.Articles.UserCreate)]
    [ProducesResponseType(typeof(Response<ArticleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<ArticleResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateArticleRequest request,
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var authorId))
            return Ok(Response<ArticleResponse>.Unauthorized());

        if (string.IsNullOrWhiteSpace(request.Title))
            return Ok(Response<ArticleResponse>.BadRequest("Title is required."));

        if (string.IsNullOrWhiteSpace(request.Content))
            return Ok(Response<ArticleResponse>.BadRequest("Content is required."));

        var article = request.MapToArticle(authorId);
        await _articleService.CreateAsync(article, cancellationToken);

        return Ok(Response<ArticleResponse>.Created(article.MapToResponse(), "Article saved successfully."));
    }

    [HttpGet(EndPoints.Articles.UserGet)]
    [ProducesResponseType(typeof(Response<ArticleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<ArticleResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var authorId))
            return Ok(Response<ArticleResponse>.Unauthorized());

        var article = await _articleService.GetByIdAsync(id, cancellationToken);
        if (article is null || article.AuthorId != authorId)
            return Ok(Response<ArticleResponse>.NotFound($"Article with ID {id} not found."));

        return Ok(Response<ArticleResponse>.Ok(article.MapToResponse()));
    }
}
