using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTasks
{
    public class ConsumerBackgroundService : BackgroundService
    {
        private readonly ILogger<ConsumerBackgroundService> _logger;
        public ConsumerBackgroundService(ILogger<ConsumerBackgroundService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("BackgroundService is running.");

               

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); // Delay for 10 seconds
            }
        }
    }
}
