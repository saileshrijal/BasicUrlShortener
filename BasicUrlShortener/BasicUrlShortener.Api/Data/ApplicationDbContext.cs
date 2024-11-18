using BasicUrlShortener.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicUrlShortener.Api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<UrlShortener> UrlShorteners { get; set; }
}