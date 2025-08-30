using Microsoft.EntityFrameworkCore;
using System.Text;
using UrlShortener.Data;
using UrlShortener.Entities;

namespace UrlShortener.Services
{
    public class UrlShorteningService : IUrlShorteningService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _baseUrl;
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly int Base = Alphabet.Length;

        public UrlShorteningService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _baseUrl = configuration["BaseUrl"]!;
        }

        public async Task<string> GenerateShortUrlAsync(string originalUrl)
        {
            // 1. Aynı URL daha önce kısaltılmış mı diye kontrol et.
            var existingMapping = await _context.UrlMappings
                .FirstOrDefaultAsync(x => x.OriginalUrl == originalUrl);

            if (existingMapping != null)
            {
                return $"{_baseUrl}/{existingMapping.ShortCode}";
            }

            // 2. Yeni bir kayıt oluştur. ShortCode başlangıçta boş.
            var urlMapping = new UrlMapping
            {
                OriginalUrl = originalUrl
            };

            // 3. Veritabanına ekle. Bu işlem `urlMapping` nesnesine bir ID atayacak.
            _context.UrlMappings.Add(urlMapping);
            await _context.SaveChangesAsync();

            // 4. Atanan ID'yi kullanarak Base62 kodunu oluştur.
            var shortCode = Encode(urlMapping.Id);

            // 5. Kaydı yeni kod ile güncelle.
            urlMapping.ShortCode = shortCode;
            await _context.SaveChangesAsync();

            return $"{_baseUrl}/{shortCode}";
        }

        // Base62 Algoritması
        private static string Encode(int id)
        {
            if (id == 0) return Alphabet[0].ToString();

            var s = new StringBuilder();
            while (id > 0)
            {
                s.Insert(0, Alphabet[id % Base]);
                id /= Base;
            }
            return s.ToString();
        }
    }
}
