using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM.Shared.Interfaces
{
    public interface IMessageConsumer
    {
        void Receive(string publisherName, string exchangeName, string queueName, string routingKey);
    }
}
