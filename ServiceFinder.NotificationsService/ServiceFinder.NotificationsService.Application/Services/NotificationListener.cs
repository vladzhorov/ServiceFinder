using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ServiceFinder.NotificationService.Application.Services
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting RabbitMQ message listener.");

            await _busControl.StartAsync(stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping RabbitMQ message listener.");

            return _busControl.StopAsync(cancellationToken);
        }
    }
}
