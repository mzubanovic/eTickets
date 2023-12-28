using eTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{

    // naša klasa inherita od DbContext od Entity-a
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // konstruktor koji proslijeđuje instancu odDbContextOptions<AppDbContext> u base konstruktor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        }

        // konfiguriramo model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // konfiguracija relacija
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new 
            {
                am.ActorId,
                am.MovieId          
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies)
                .HasForeignKey(m => m.ActorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
        
        //Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        //Shopping cart related tables
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
