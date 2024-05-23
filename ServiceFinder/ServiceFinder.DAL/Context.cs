using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceFinder.DAL.Entites;

namespace ServiceFinder.DAL
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<UserProfileEntity> UserProfile { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<ServiceCategoryEntity> ServiceCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            System.Diagnostics.Debug.WriteLine(_configuration);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServiceEntity>()
                .HasOne(s => s.UserProfile)
                .WithMany(p => p.Services)
                .HasForeignKey(s => s.UserProfileID);

            modelBuilder.Entity<ServiceEntity>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.ServiceCategoryID);

            modelBuilder.Entity<ReviewEntity>()
                .HasOne(r => r.Service)
                .WithMany(s => s.Reviews)
                .HasForeignKey(r => r.ServiceId);
        }
    }
}