using UrlShortener.DTOs;

namespace UrlShortener.Services
{
    public interface IUrlShorteningService
    {
        Task<string> GenerateShortUrlAsync(string originalUrl);
    }
}
