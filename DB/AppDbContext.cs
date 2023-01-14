using Microsoft.EntityFrameworkCore;
using Models;

namespace DB;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Monster> Monsters { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var mimic = new Monster
        {
            Id = 1,
            Name = "Mimic",
            HitPoints = 58,
            AttackModifier = 18,
            AttackPerRound = 1,
            Damage = "9d8",
            DamageModifier = 3,
            ArmorClass = 12
        };

        var arasta = new Monster
        {
            Id = 2,
            Name = "Arasta",
            HitPoints = 300,
            AttackModifier = 144,
            AttackPerRound = 1,
            Damage = "24d12",
            DamageModifier = 7,
            ArmorClass = 19
        };

        var bugbearChief = new Monster
        {
            Id = 3,
            Name = "Bugbear Chief",
            HitPoints = 65,
            AttackModifier = 20,
            AttackPerRound = 1,
            Damage = "10d8",
            DamageModifier = 3,
            ArmorClass = 17
        };

        var agony = new Monster
        {
            Id = 4,
            Name = "Agony",
            HitPoints = 45,
            AttackModifier = 0,
            AttackPerRound = 1,
            Damage = "10d8",
            DamageModifier = -2,
            ArmorClass = 11
        };

        modelBuilder.Entity<Monster>().HasData(mimic, arasta, bugbearChief, agony);
    }
}