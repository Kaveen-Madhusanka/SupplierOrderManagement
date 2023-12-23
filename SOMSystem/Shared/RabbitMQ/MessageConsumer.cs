using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SOM.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class MessageConsumer: IMessageConsumer
    {
        public void Receive(string publisherName, string exchangeName, string queueName, string routingKey)
        {

            ConnectionFactory factory = new()
            {
                Uri = new Uri(MapQueueUri(publisherName)),
                ClientProvidedName = publisherName
            };

            IConnection con = factory.CreateConnection();

            IModel channel = con.CreateModel();

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);
            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (sender, args) =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5)); // Simulating processing time
                var body = args.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Message Received: {message}");
                channel.BasicAck(args.DeliveryTag, false);
            };

            string consumerTag = channel.BasicConsume(queueName, false, consumer);

            channel.BasicCancel(consumerTag);
            //channel.Close(); // need check this
            //con.Close();
        }

        private string MapQueueUri(string publisherName)
        {
            string QueueName = string.Empty;
            switch (publisherName)
            {
                case "supplier-service":
                    QueueName = "amqp://guest:guest@localhost:5672";// this needs read fromm appsettings
                    break;
                default:
                    break;
            }
            return QueueName;
        }
    }
}
