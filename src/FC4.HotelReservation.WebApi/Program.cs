using FC4.HotelReservation.Application;
using FC4.HotelReservation.Domain;
using FC4.HotelReservation.Infrastructure;
using FC4.HotelReservation.WebApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDomainServices()
    .AddRepositories()
    .AddUseCases();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.Run();