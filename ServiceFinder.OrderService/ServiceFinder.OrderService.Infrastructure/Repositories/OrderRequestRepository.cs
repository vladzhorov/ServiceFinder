using ServiceFinder.OrderService.Domain.Interfaces;
using ServiceFinder.OrderService.Domain.Models;

namespace ServiceFinder.OrderService.Infrastructure.Repositories
{
    public class OrderRequestRepository : Repository<OrderRequest>, IOrderRequestRepository
    {
        public OrderRequestRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }
    }
}