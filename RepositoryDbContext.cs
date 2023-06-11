using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence;

public sealed class RepositoryDbContext : DbContext
{    
    public RepositoryDbContext(
        DbContextOptions<RepositoryDbContext> options)
        : base(options)
    {
    }

    public DbSet<User?> Users { get; set; }
    public DbSet<Auth?> Auth { get; set; }
    public DbSet<TouristPlace?> TouristPlaces { get; set; }
    public DbSet<Route?> Routes { get; set; }
    public DbSet<Joint?> Joints { get; set; }
    public DbSet<UserRole?> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
}