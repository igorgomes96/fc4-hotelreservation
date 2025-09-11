using System.Text.Json;
using FC4.HotelReservation.Shared.Infrastructure;
using FC4.HotelReservation.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Testcontainers.PostgreSql;

namespace FC4.HotelReservation.IntegrationTests;

public partial class WebApiFixture : WebApplicationFactory<Program>
{
    public PostgreSqlContainer PostgresDb { get; } =
        new PostgreSqlBuilder()
            .WithDatabase("hotel_reservation")
            .Build();

    public JsonSerializerOptions JsonSettings { get; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "IntegrationTests");
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "IntegrationTests");
        
        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:HotelReservationDb", PostgresDb.GetConnectionString() },
            }!);
        });
    }
    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        PostgresDb.StartAsync().GetAwaiter().GetResult();
        
        var host = base.CreateHost(builder);
        
        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
        dbContext.Database.Migrate();
        
        return host;
    }
    
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            PostgresDb.DisposeAsync().AsTask().Wait();
        }
        base.Dispose(disposing);
    }
}

[CollectionDefinition(nameof(WebApiFixture))]
public class CustomWebApplicationFactoryCollectionFixture : ICollectionFixture<WebApiFixture>;