using Newtonsoft.Json;
using RabbitMQ.Client;
using SOM.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class MessagePublisher: IMessagePublisher
    {

        public bool Publish<T>(string publisherName, string exchangeName, string queueName, string routingKey, T message)
        {
            ConnectionFactory factory = new();
            factory.Uri = new Uri(MapQueueUri(publisherName));
            factory.ClientProvidedName = publisherName;

            IConnection con = factory.CreateConnection();

            IModel channel = con.CreateModel();

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);
            channel.BasicQos(0, 1, false);

            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);

            Thread.Sleep(1000);

            channel.Close();
            con.Close();

            return true;
        }

        private string MapQueueUri(string publisherName)
        {
            string QueueName = string.Empty;
            switch (publisherName)
            {
                case "Prodct":
                    QueueName = "amqp://guest:guest@localhost:5672";// this needs read fromm appsettings
                    break;
                default:
                    break;
            }
            return QueueName;
        }


    }
}
