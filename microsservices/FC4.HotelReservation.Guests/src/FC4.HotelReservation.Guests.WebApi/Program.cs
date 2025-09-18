using FC4.HotelReservation.Guests.Application;
using FC4.HotelReservation.Guests.Infra;
using FC4.HotelReservation.Guests.WebApi;
using FC4.HotelReservation.Guests.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddGuestsUseCases()
    .AddGuestsRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers["x-app-id"] = "guests-microservice";
            return Task.CompletedTask;
        });
        await next();
    })
    .UseExceptionHandler();

app.MapGroup("/v1/guests")
    .MapGuestsApi()
    .WithOpenApi()
    .WithTags("Guests");

app.Run();

namespace FC4.HotelReservation.Guests.WebApi
{
    public partial class Program;
}