using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;
using ServiceFinder.OrderService.Infrastructure.Repositories;

namespace ServiceFinder.OrderService.Infrastructure.Persistence.Repositories
{
    public class OrderRequestRepository : Repository<OrderRequest>, IOrderRequestRepository
    {
        public OrderRequestRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }
    }
}