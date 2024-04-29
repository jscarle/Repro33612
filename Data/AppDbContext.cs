using Microsoft.EntityFrameworkCore;

namespace Repro33612.Data;

public sealed class AppDbContext : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<CountryTranslation> CountryTranslations { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<StateTranslation> StateTranslations { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<CityTranslation> CityTranslations { get; set; }

    public AppDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_100_CI_AI_SC_UTF8");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<CountryId>().HaveConversion<CountryIdConverter>();
        configurationBuilder.Properties<StateId>().HaveConversion<StateIdConverter>();
        configurationBuilder.Properties<CityId>().HaveConversion<CityIdConverter>();
        configurationBuilder.Properties<CultureId>().HaveConversion<CultureIdConverter>();
    }
}
