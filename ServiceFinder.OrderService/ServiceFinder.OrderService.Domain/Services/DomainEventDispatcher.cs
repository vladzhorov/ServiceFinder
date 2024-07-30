using MediatR;
using ServiceFinder.OrderService.Domain.Interfaces;

namespace ServiceFinder.OrderService.Domain.Services
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        public void Dispatch<T>(T domainEvent) where T : INotification
        {
            Console.WriteLine($"Event dispatched: {domainEvent.GetType().Name}");
        }
    }
}