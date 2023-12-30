using SOM.Shared.Interfaces;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RabbitMQ;

// TODO: need to refact

public class MessagePublisher: IMessagePublisher
{
    private IConnection _connection;
    private IModel _channel;
    private const string ExchangeName = "som-exchange";

    public MessagePublisher(IConnectionFactory connectionFactory)
    {
        // TODO: Need to handle multiple connection and multiple channel issue
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(ExchangeName, ExchangeType.Topic);
    }
        
    public void Publish<TEventEnum, TMessage>(TEventEnum eventEnum, TMessage message)
    {
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        _channel.BasicPublish(ExchangeName, eventEnum?.ToString(), null,body);
    }
}