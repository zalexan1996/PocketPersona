using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PP.Core.Services;
using PP.Domain.Entities;

namespace PP.Infrastructure;

public class PersonaDbContext(IConfigurationRoot config) : DbContext, IDbService
{
    void IDbService.AddRange<T>(IEnumerable<T> entities) where T : class => AddRange(entities);

    void IDbService.RemoveRange<T>(IEnumerable<T> entities) where T : class => RemoveRange(entities);

    Task IDbService.SaveChanges(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);

    void IDbService.Add<T>(T entity) => Add(entity);

    void IDbService.Remove<T>(T entity) => Remove(entity);

    IQueryable<T> IDbService.Set<T>() => Set<T>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Game).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("Default"))
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }

}