using FidelityCard.Lib.Models;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;

namespace FidelityCard.Srv.Data;

public class FidelityCardDbContext(DbContextOptions<FidelityCardDbContext> options) : DbContext(options)
{
    public DbSet<Fidelity> Fidelity { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fidelity>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Fidelity>()
              .HasIndex(u => u.CdFidelity)
              .IsUnique();
    }

}

