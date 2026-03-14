using System.Security.Claims;
using Contract.Common;
using Contract.Requests.Articles;
using Contract.Responses.Articles;
using Contract.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plume.Application.Mappings;
using Plume.Domain.Enums;

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

    [HttpGet(EndPoints.Articles.UserGetAll)]
    [ProducesResponseType(typeof(Response<List<ArticleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var authorId))
            return Ok(Response<List<ArticleResponse>>.Unauthorized());

        var articles = await _articleService.GetAllByAuthorIdAsync(authorId, cancellationToken);
        var response = articles.Select(a => a.MapToResponse()).ToList();

        return Ok(Response<List<ArticleResponse>>.Ok(response));
    }

    [HttpPut(EndPoints.Articles.UserUpdate)]
    [ProducesResponseType(typeof(Response<ArticleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<ArticleResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateArticleRequest request,
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var authorId))
            return Ok(Response<ArticleResponse>.Unauthorized());

        if (string.IsNullOrWhiteSpace(request.Title))
            return Ok(Response<ArticleResponse>.BadRequest("Title is required."));

        var article = await _articleService.GetByIdAsync(id, cancellationToken);
        if (article is null || article.AuthorId != authorId)
            return Ok(Response<ArticleResponse>.NotFound($"Article with ID {id} not found."));

        request.ApplyUpdate(article);
        var updated = await _articleService.UpdateAsync(article, cancellationToken);
        if (updated is null)
            return Ok(Response<ArticleResponse>.ServerError("Failed to update article."));

        return Ok(Response<ArticleResponse>.Ok(updated.MapToResponse()));
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

    [HttpPut(EndPoints.Articles.UserPublish)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Publish([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var authorId))
            return Ok(Response<bool>.Unauthorized());

        var article = await _articleService.GetByIdAsync(id, cancellationToken);
        if (article is null || article.AuthorId != authorId)
            return Ok(Response<bool>.NotFound($"Article with ID {id} not found."));

        var success = await _articleService.ChangeArticleStatusAsync(id, ArticleStatus.Published, cancellationToken);
        if (!success)
            return Ok(Response<bool>.ServerError("ލިޔުން ޝާއިއުކުރެވޭ ވަރެއް ނުވި."));

        return Ok(Response<bool>.Ok(true, "ލިޔުން ޝާއިއުކުރެވިއްޖެ."));
    }

    [HttpPut(EndPoints.Articles.UserUnlist)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Unlist([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var authorId))
            return Ok(Response<bool>.Unauthorized());

        var article = await _articleService.GetByIdAsync(id, cancellationToken);
        if (article is null || article.AuthorId != authorId)
            return Ok(Response<bool>.NotFound($"Article with ID {id} not found."));

        var success = await _articleService.ChangeArticleStatusAsync(id, ArticleStatus.Unlisted, cancellationToken);
        if (!success)
            return Ok(Response<bool>.ServerError("ލިޔުން އަންލިސްޓުކުރެވޭ ވަރެއް ނުވި."));

        return Ok(Response<bool>.Ok(true, "ލިޔުން އަންލިސްޓުކުރެވިއްޖެ."));
    }

    [HttpDelete(EndPoints.Articles.UserDelete)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var authorId))
            return Ok(Response<bool>.Unauthorized());

        var article = await _articleService.GetByIdAsync(id, cancellationToken);
        if (article is null || article.AuthorId != authorId)
            return Ok(Response<bool>.NotFound($"Article with ID {id} not found."));

        var success = await _articleService.ChangeArticleStatusAsync(id, ArticleStatus.Deleted, cancellationToken);
        if (!success)
            return Ok(Response<bool>.ServerError("ލިޔުން ޑިލީޓުކުރެވޭ ވަރެއް ނުވި."));

        return Ok(Response<bool>.Ok(true, "ލިޔުން ޑިލީޓުކުރެވިއްޖެ."));
    }
}
