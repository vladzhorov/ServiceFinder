using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceFinder.DAL.Entites;
using ServiceFinder.DAL.Interceptors;

namespace ServiceFinder.DAL
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }

        public DbSet<UserProfileEntity> UserProfile { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<AssistanceEntity> Assistances { get; set; }
        public DbSet<AssistanceCategoryEntity> AssistanceCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                          .AddInterceptors(new UpdateAuditableInterceptor())
                          .AddInterceptors(new UpdateRatingInterceptor())
                          .AddInterceptors(new SoftDeleteInterceptor());
            System.Diagnostics.Debug.WriteLine(_configuration);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfileEntity>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<AssistanceEntity>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<AssistanceCategoryEntity>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<ReviewEntity>().HasQueryFilter(e => !e.IsDeleted);
        }
    }
}