﻿namespace ServiceFinder.OrderService.Domain.Messaging.RabbitMQConfigurations
{
    public class RabbitMQConnectionConfiguration
    {
        public string HostName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
