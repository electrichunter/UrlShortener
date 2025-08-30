using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [Route("api")]
    [ApiController]
    public class ShortenerController : ControllerBase
    {
        private readonly IUrlShorteningService _shorteningService;

        public ShortenerController(IUrlShorteningService shorteningService)
        {
            _shorteningService = shorteningService;
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] ShortenUrlRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shortUrl = await _shorteningService.GenerateShortUrlAsync(request.Url);

            var response = new ShortenUrlResponse { ShortUrl = shortUrl };
            return Ok(response);
        }
    }
}
