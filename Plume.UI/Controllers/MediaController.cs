using System.Net.Http.Headers;
using Contract.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Plume.UI.Controllers;

[Authorize]
[ApiController]
public class MediaController : ControllerBase
{
    private static readonly HashSet<string> AllowedContentTypes =
    [
        "image/jpeg",
        "image/png",
        "image/webp",
        "image/gif"
    ];

    private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

    private readonly IWebHostEnvironment _env;

    public MediaController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpPost(EndPoints.Media.UploadArticleImage)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UploadArticleImage(IFormFile file, CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
            return Ok(Response<string>.BadRequest("No file provided."));

        if (!AllowedContentTypes.Contains(file.ContentType.ToLowerInvariant()))
            return Ok(Response<string>.BadRequest("Only JPEG, PNG, WebP, or GIF images are allowed."));

        if (file.Length > MaxFileSizeBytes)
            return Ok(Response<string>.BadRequest("File size must not exceed 5 MB."));

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid():N}{ext}";
        var uploadDir = Path.Combine(_env.ContentRootPath, "uploads", "articles");
        Directory.CreateDirectory(uploadDir);

        var filePath = Path.Combine(uploadDir, fileName);
        await using var stream = System.IO.File.Create(filePath);
        await file.CopyToAsync(stream, cancellationToken);

        return Ok(Response<string>.Ok($"/uploads/articles/{fileName}", "Image uploaded successfully."));
    }

    // Editor.js Image tool expects: { success: 1, file: { url: "..." } }
    [HttpPost(EndPoints.Media.EditorJsImageUpload)]
    public async Task<IActionResult> EditorJsImageUpload(IFormFile image, CancellationToken cancellationToken)
    {
        if (image is null || image.Length == 0)
            return Ok(new { success = 0, message = "No file provided." });

        if (!AllowedContentTypes.Contains(image.ContentType.ToLowerInvariant()))
            return Ok(new { success = 0, message = "Only JPEG, PNG, WebP, or GIF images are allowed." });

        if (image.Length > MaxFileSizeBytes)
            return Ok(new { success = 0, message = "File size must not exceed 5 MB." });

        var ext = Path.GetExtension(image.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid():N}{ext}";
        var uploadDir = Path.Combine(_env.ContentRootPath, "uploads", "articles");
        Directory.CreateDirectory(uploadDir);

        var filePath = Path.Combine(uploadDir, fileName);
        await using var stream = System.IO.File.Create(filePath);
        await image.CopyToAsync(stream, cancellationToken);

        return Ok(new { success = 1, file = new { url = $"/uploads/articles/{fileName}" } });
    }
}
