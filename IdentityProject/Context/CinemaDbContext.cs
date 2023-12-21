using IdentityProject.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.Context
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext()
        {
        }

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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplyDiscount>()
                .HasKey(ad => new { ad.dis_id, ad.bi_id });

            modelBuilder.Entity<ChooseType>()
                .HasKey(ct => new { ct.type_id, ct.mv_id });

            modelBuilder.Entity<Month>()
               .HasKey(mo => new { mo.mre_id, mo.mre_yre_id });

            modelBuilder.Entity<Seat>()
               .HasKey(se => new { se.st_id, se.r_id });

            modelBuilder.Entity<Slot>()
               .HasKey(sl => new { sl.sl_id, sl.r_id, sl.mv_id });

            modelBuilder.Entity<Ticket>()
               .HasKey(tk => new { tk.tk_id, tk.sl_id, tk.st_id });


        }
    }
}
