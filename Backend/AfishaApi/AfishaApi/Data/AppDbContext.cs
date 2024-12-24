using Microsoft.EntityFrameworkCore;
using AfishaApi.Models; // Убедитесь, что пространство имен правильно указано

namespace AfishaApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet для пользователей
        public DbSet<UserEntityDb> Users { get; set; }
        public DbSet<BookingEntityDb> Bookings { get; set; }
        public DbSet<EventEntityDb> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
