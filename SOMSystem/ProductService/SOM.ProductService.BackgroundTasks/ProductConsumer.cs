using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SOM.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTasks
{
    public class ProductConsumer : BackgroundService
    {
        private readonly ILogger<ProductConsumer> _logger;
        private readonly IMessageConsumer _messageConsumer;

        public ProductConsumer(ILogger<ProductConsumer> logger, IMessageConsumer messageConsumer)
        {
            _logger = logger;
            _messageConsumer = messageConsumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var publisherName = "supplier-service";
            string exchangeName = "DemoExchange";
            string routingKey = "demo-routing-key";
            string queueName = "DemoQueue";
            //var exchangeName = "som-exchanger";
            //var queueName = "som-queue";
            //var routingKey = "supplier-route-key";

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("BackgroundService is running.");
                _messageConsumer.Receive(publisherName,exchangeName,queueName,routingKey);
               

                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken); // Delay for 10 seconds
            }
        }
    }
}
