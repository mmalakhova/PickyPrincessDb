using Microsoft.EntityFrameworkCore;
using PickyPrincessDb.entities;

namespace PickyPrincessDb.db;

public class HallContext : DbContext
{
    internal HallContext()
    {
        Database.EnsureCreated();
    }

    public HallContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Contender> Contenders => Set<Contender>();
    public DbSet<Attempt> Attempts => Set<Attempt>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        { 
            optionsBuilder.UseNpgsql(
                "Port=54330;Host=localhost;Username=postgres;Password=P@ssw0rd;Database=db");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Attempt>()
            .HasMany(a => a.Contenders)
            .WithOne(c => c.Attempt);
    }
}