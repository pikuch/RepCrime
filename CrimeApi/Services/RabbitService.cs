using Newtonsoft.Json;
using RabbitMQ.Client;
using RepCrimeCommon.Dtos;
using System.Text;

namespace CrimeApi.Services;

public class RabbitService : IRabbitService
{
    private readonly IConnection _connection;
    private readonly string _queueName;
    private readonly string _exchangeName;

    public RabbitService(IConfiguration configuration)
    {
        var factory = new ConnectionFactory();
        factory.HostName = configuration["Rabbit"];
        _connection = factory.CreateConnection();
        _queueName = configuration["QueueName"];
        _exchangeName = configuration["ExchangeName"];
    }

    public bool SendMessage(CrimeEventReadDto crimeEvent)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
        channel.QueueBind(_queueName, _exchangeName, "");
        string message = JsonConvert.SerializeObject(crimeEvent);
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(_exchangeName, "", null, body);
        return true;
    }
}
