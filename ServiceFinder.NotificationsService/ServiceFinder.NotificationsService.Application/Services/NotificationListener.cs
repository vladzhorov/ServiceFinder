using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ServiceFinder.NotificationService.Application.BackgroundServices
{
    public class NotificationListener : BackgroundService
    {
        private readonly IBusControl _busControl;
        private readonly ILogger<NotificationListener> _logger;

        public NotificationListener(IBusControl busControl, ILogger<NotificationListener> logger)
        {
            _busControl = busControl;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting RabbitMQ message listener.");

            return _busControl.StartAsync(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping RabbitMQ message listener.");

            return _busControl.StopAsync(cancellationToken);
        }
    }
}
