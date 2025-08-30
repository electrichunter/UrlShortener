using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;

namespace UrlShortener.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<UrlMapping> UrlMappings { get; set; }
    }
}
