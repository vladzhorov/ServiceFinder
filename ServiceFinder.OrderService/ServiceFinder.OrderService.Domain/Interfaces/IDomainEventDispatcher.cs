
using MediatR;

namespace ServiceFinder.OrderService.Domain.Interfaces
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<T>(T domainEvent) where T : INotification;
    }
}