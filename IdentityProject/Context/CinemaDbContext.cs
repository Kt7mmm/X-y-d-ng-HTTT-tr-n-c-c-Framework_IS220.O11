using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Context
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
        }

        public DbSet<MovieType> MovieTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ChooseType> ChooseTypes { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<ApplyDiscount> ApplyDiscounts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Month> Months { get; set; }
    }
}
