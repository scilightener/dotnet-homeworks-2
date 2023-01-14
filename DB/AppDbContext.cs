using Microsoft.EntityFrameworkCore;
using Models;

namespace DB;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Monster> Monsters { get; set; }
}