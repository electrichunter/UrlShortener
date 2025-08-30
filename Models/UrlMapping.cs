using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Entities
{
    // ShortCode'un benzersiz olmasını ve hızlı aranabilmesi için bir index ekliyoruz.
    [Index(nameof(ShortCode), IsUnique = true)]
    public class UrlMapping
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; } = string.Empty;
        
        // İlk kayıtta boş olacak, sonra güncellenecek.
        public string ShortCode { get; set; } = string.Empty; 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
