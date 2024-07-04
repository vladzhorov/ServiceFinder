using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceFinder.OrderService.Domain.Models;

namespace ServiceFinder.OrderService.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public OrderDbContext(DbContextOptions<OrderDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRequest> OrderRequests { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            System.Diagnostics.Debug.WriteLine(_configuration);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
