using FC4.HotelReservation.Reservations.Application;
using FC4.HotelReservation.Reservations.Consumers;
using FC4.HotelReservation.Reservations.Infra.Data;
using FC4.HotelReservation.Shared.Infrastructure;
using FC4.HotelReservation.WebApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddRepositories()
    .AddReservationsUseCases()
    .AddReservationsRepositories()
    .AddReservationsConsumers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();


app.Run();

namespace FC4.HotelReservation.WebApi
{
    public partial class Program;
}