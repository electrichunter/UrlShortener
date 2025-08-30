using System.ComponentModel.DataAnnotations;

namespace UrlShortener.DTOs
{
    public class ShortenUrlRequest
    {
        [Required(ErrorMessage = "URL alanı boş olamaz.")]
        [Url(ErrorMessage = "Lütfen geçerli bir URL giriniz.")]
        public string Url { get; set; } = string.Empty;
    }
}
