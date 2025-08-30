using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Servislerin Eklenmesi (Dependency Injection) ---

// 1. Veritabanı bağlantısı
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Kendi yazdığımız iş mantığı servisi
builder.Services.AddScoped<IUrlShorteningService, UrlShorteningService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- HTTP Pipeline'ının Yapılandırılması ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Controller'daki endpoint'leri haritala (örn: /api/shorten)
app.MapControllers();

// --- Yönlendirme Endpoint'i (Minimal API ile) ---
// Bu endpoint, kısa koda gelen istekleri yakalayıp orijinal URL'ye yönlendirir.
app.MapGet("/{shortCode}", async (string shortCode, ApplicationDbContext dbContext) =>
{
    var urlMapping = await dbContext.UrlMappings
        .FirstOrDefaultAsync(u => u.ShortCode == shortCode);

    if (urlMapping is null)
    {
        return Results.NotFound("Kısa URL bulunamadı.");
    }

    return Results.Redirect(urlMapping.OriginalUrl);
});


app.Run();
