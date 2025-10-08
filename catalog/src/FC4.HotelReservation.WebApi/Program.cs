using FC4.HotelReservation.Catalog.Application;
using FC4.HotelReservation.Catalog.Domain;
using FC4.HotelReservation.Catalog.Infra;
using FC4.HotelReservation.Shared.Infrastructure;
using FC4.HotelReservation.WebApi;
using FC4.HotelReservation.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddDomainServices()
    .AddRepositories()
    .AddCatalogUseCases()
    .AddCatalogRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();

app.MapGroup("/v1/hotels")
    .MapHotelsApi()
    .WithOpenApi()
    .WithTags("Hotel");

app.MapGroup("/v1/rooms")
    .MapRoomsApi()
    .WithOpenApi()
    .WithTags("Rooms");

app.MapGroup("/v1/rates")
    .MapRatesApi()
    .WithOpenApi()
    .WithTags("Rates");

app.Run();

namespace FC4.HotelReservation.WebApi
{
    public partial class Program;
}