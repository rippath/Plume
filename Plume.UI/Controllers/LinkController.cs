using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Plume.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinkController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LinkController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("fetchUrl")]
    public async Task<IActionResult> FetchUrl([FromQuery] string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return Ok(new { success = 0, message = "URL is required" });
        }

        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(10);
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; PlumeBot/1.0)");

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return Ok(new { success = 0, message = "Failed to fetch URL" });
            }

            var html = await response.Content.ReadAsStringAsync();
            var meta = ExtractMetadata(html, url);

            return Ok(new
            {
                success = 1,
                meta = meta
            });
        }
        catch (Exception ex)
        {
            return Ok(new { success = 0, message = ex.Message });
        }
    }

    private object ExtractMetadata(string html, string url)
    {
        var title = ExtractMetaTag(html, "og:title")
                    ?? ExtractMetaTag(html, "twitter:title")
                    ?? ExtractTitleTag(html)
                    ?? "No title";

        var description = ExtractMetaTag(html, "og:description")
                         ?? ExtractMetaTag(html, "twitter:description")
                         ?? ExtractMetaTag(html, "description")
                         ?? "";

        var imageUrl = ExtractMetaTag(html, "og:image")
                      ?? ExtractMetaTag(html, "twitter:image")
                      ?? "";

        return new
        {
            title = title,
            description = description,
            image = new
            {
                url = imageUrl
            }
        };
    }

    private string? ExtractMetaTag(string html, string property)
    {
        var patterns = new[]
        {
            $@"<meta\s+property=[""']{property}[""']\s+content=[""']([^""']+)[""']",
            $@"<meta\s+name=[""']{property}[""']\s+content=[""']([^""']+)[""']",
            $@"<meta\s+content=[""']([^""']+)[""']\s+property=[""']{property}[""']",
            $@"<meta\s+content=[""']([^""']+)[""']\s+name=[""']{property}[""']"
        };

        foreach (var pattern in patterns)
        {
            var match = Regex.Match(html, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
        }

        return null;
    }

    private string? ExtractTitleTag(string html)
    {
        var match = Regex.Match(html, @"<title>([^<]+)</title>", RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value.Trim() : null;
    }
}