using Microsoft.EntityFrameworkCore;

namespace QAFoodDb
{
    public class QAFoodContext : DbContext, IQAFoodDb
    {
        public QAFoodContext(DbContextOptions<QAFoodContext> options) : base(options) { }

        public QAFoodContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
            });
            modelBuilder.Entity<Food>()
                        .Property(f => f.FoodName)
                        .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionstr = @"server=LAPTOP-5URJHDOL; database=QAFoodDb; user id=sa; password=password123;";
                optionsBuilder
                    .UseSqlServer(connectionstr, providerOptions => providerOptions.CommandTimeout(60))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<ReviewDetails> ReviewDetails { get; set; }
    }
}
