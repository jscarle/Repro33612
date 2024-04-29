using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repro33612.Data;

namespace Repro33612;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddSingleton<EfQueryRunner>();
        builder.Services.AddSingleton<HostRunner>();
        builder.Services.AddDbContext<AppDbContext>(
            options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            }
        );

        var app = builder.Build();

        var dbContext = app.Services.GetRequiredService<AppDbContext>();
        await dbContext.Database.EnsureCreatedAsync();

        var runner = app.Services.GetRequiredService<HostRunner>();

        await runner.RunAsync(app);
    }
}
