# UrlShortener

Basit bir .NET 9 URL kısaltma servisi.

Özellikler
- Orijinal URL'leri kaydeder ve Base62 ile kısa kod üretir.
- Kısa kod ile yönlendirir (redirect).
- SQLite ile çalışan EF Core veri tabanı.
- Swagger destekli API keşfi.

Kaynaklar
- [Program.cs](Program.cs) — uygulama başlangıcı ve yönlendirme endpoint'i.
- [`UrlShortener.Services.UrlShorteningService`](Services/UrlShorteningService.cs) — kısa URL üretme mantığı.
- [`UrlShortener.Services.IUrlShorteningService`](Services/IUrlShorteningService.cs) — servis arayüzü.
- [`UrlShortener.Data.ApplicationDbContext`](Data/ApplicationDbContext.cs) — EF DbContext.
- [`UrlShortener.Entities.UrlMapping`](Models/UrlMapping.cs) — veri modeli.
- [`UrlShortener.Controllers.ShortenerController`](Controllers/ShortenController.cs) — /api/shorten endpoint'i.
- DTO'lar: [`UrlShortener.DTOs.ShortenUrlRequest`](DTOs/ShortenUrlRequest.cs), [`UrlShortener.DTOs.ShortenUrlResponse`](DTOs/ShortenUrlResponse.cs).
- Konfigürasyon: [appsettings.json](appsettings.json)
- Proje dosyası: [UrlShortener.csproj](UrlShortener.csproj)

Gereksinimler
- .NET 9 SDK
- dotnet-ef (migrate/uygulamak isterseniz)

Başlangıç (lokalde)
1. Paketleri yükleyin:
   dotnet restore

2. Veritabanını oluşturun (migrations mevcut):
   dotnet tool install --global dotnet-ef   (eğer yüklü değilse)
   dotnet ef database update

3. Uygulamayı çalıştırın:
   dotnet run

Konfigürasyon
- Base URL ve connection string [appsettings.json](appsettings.json) içinde:
  - ConnectionStrings: DefaultConnection (ör: Data Source=urlshortener.db)
  - BaseUrl (ör: http://localhost:5122)

API
- POST /api/shorten
  - İstek gövdesi: { "Url": "https://ornek.com" }
  - Dönen: { "ShortUrl": "http://localhost:5122/abc" }
  - Controller: [`UrlShortener.Controllers.ShortenerController`](Controllers/ShortenController.cs)

- GET /{shortCode}
  - Kısa kodu alıp orijinal URL'ye yönlendirir (Program.cs içindeki minimal API).
  - Kod: [Program.cs](Program.cs)

Notlar
- Model: [`UrlShortener.Entities.UrlMapping`](Models/UrlMapping.cs) — ShortCode için benzersiz index mevcut.
- Kısa kod üretimi Base62 ile [`UrlShortener.Services.UrlShorteningService`](Services/UrlShorteningService.cs) içinde yapılır.
- Projeyi kendi alan adınızda çalıştırmak isterseniz `BaseUrl` değerini güncelleyin.

Katkı
İssue/PR gönderin.

Lisans
Projede lisans belirtilmemiştir — kullanım
