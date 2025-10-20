using System.Text.Json;
using Confluent.Kafka;
using FC4.HotelReservation.Reservations.Consumers.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FC4.HotelReservation.Reservations.Consumers.Consumers;

public class PaymentChangedConsumer : BackgroundService
{
    private readonly ILogger<PaymentChangedConsumer> _logger;
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly IServiceProvider _serviceProvider;

    public PaymentChangedConsumer(ILogger<PaymentChangedConsumer> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:29092",
            GroupId = "payments-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("postgres.public.payments");

        var jsonSettings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var cr = _consumer.Consume(stoppingToken);
                _logger.LogInformation("Mensagem recebida: {value}", cr.Message.Value);
                var message = JsonSerializer.Deserialize<EventModel<PaymentChangedEventModel>>(
                    cr.Message.Value, jsonSettings);
                if (message?.Payload.Op != "u") continue;
                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var processPaymentStatusInput = message.Payload.After!.ToProcessPaymentStatusInput();
                await mediator.Send(processPaymentStatusInput, stoppingToken);
            }
            catch (ConsumeException e)
            {
                _logger.LogError(e, "Erro ao consumir mensagem");
            }

            await Task.Delay(500, stoppingToken);
        }
    }

    public override void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
        base.Dispose();
    }
}