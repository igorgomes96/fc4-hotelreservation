using FC.HotelReservation.Payments.Application;
using FC4.HotelReservation.Catalog.Application;
using FC4.HotelReservation.Catalog.Domain;
using FC4.HotelReservation.Catalog.Infra.Data;
using FC4.HotelReservation.Infrastructure;
using FC4.HotelReservation.Payments.Consumers;
using FC4.HotelReservation.Reservations.Adapters;
using FC4.HotelReservation.Reservations.Application;
using FC4.HotelReservation.Reservations.Consumers;
using FC4.HotelReservation.WebApi;
using FC4.HotelReservation.WebApi.Endpoints;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddCatalogDomainServices()
    .AddCatalogUseCases()
    .AddCatalogRepositories()
    .AddRepositories()
    .AddPaymentsUseCases()
    .AddReservationsUseCases()
    .AddReservationAdapters()
    .AddMassTransit(configurator =>
    {
        configurator
            .AddReservationConsumers()
            .AddPaymentConsumers()
            .UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
    });

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

app.MapGroup("/v1/payments")
    .MapPaymentsApi()
    .WithOpenApi()
    .WithTags("Payments");

app.MapGroup("/v1/reservations")
    .MapReservationsApi()
    .WithOpenApi()
    .WithTags("Reservations");

app.MapGroup("/v1/rates")
    .MapRatesApi()
    .WithOpenApi()
    .WithTags("rates");

app.Run();

namespace FC4.HotelReservation.WebApi
{
    public partial class Program;
}