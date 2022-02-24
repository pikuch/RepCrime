using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RepCrimeCommon.Dtos;
using System.Text;

namespace CrimeFeedbackService.Services;

public class RabbitReceiver : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly string _queueName;
    private readonly string _exchangeName;
    private readonly ILogger _logger;
    private ConnectionFactory? _factory;
    private IConnection _connection = null!;
    private IModel? _channel;
    public RabbitReceiver(IConfiguration configuration, ILogger<RabbitReceiver> logger)
    {
        _configuration = configuration;
        _queueName = configuration["QueueName"];
        _exchangeName = configuration["ExchangeName"];
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _factory = new ConnectionFactory
        {
            HostName = _configuration["Rabbit"],
            DispatchConsumersAsync = true
        };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName,
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

        return base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        _connection.Close();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var queueConsumer = new AsyncEventingBasicConsumer(_channel);

        queueConsumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var crimeEvent = JsonConvert.DeserializeObject<CrimeEventReadDto>(message);

            Console.WriteLine($"Sending an email to {crimeEvent.ReporterEmail} about their report getting an officer.");
        };

        _channel.BasicConsume(_queueName, true, queueConsumer);
        await Task.CompletedTask;
    }
}
