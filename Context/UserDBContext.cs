using E_COMMERCE_WEBSITE.Models;
using E_COMMERCE_WEBSITE.Repositories.categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace E_COMMERCE_WEBSITE.Context
{
    public class UserDBContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public UserDBContext(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(p => p.Role)
            .HasDefaultValue("user");

            modelBuilder.Entity<Category>()
                .HasMany(a => a.product)
                .WithOne(a => a.categ)
                .HasForeignKey(c => c.categid);

            modelBuilder.Entity<User>()
                .HasOne(e => e.carts)
                .WithOne(a => a.user)
                .HasForeignKey<Cart>(c => c.userid);

            modelBuilder.Entity<CartItem>()
                .HasOne(e => e.products)
               .WithMany(a => a.cartitem)              
               .HasForeignKey(c => c.ProdId);

            modelBuilder.Entity<Cart>()
               .HasMany(e => e.cartItems)
               .WithOne(a => a.cartss)
               .HasForeignKey(c => c.cartid);

            modelBuilder.Entity<WishList>()
             .HasOne(e => e.Users)
             .WithMany(a => a.WishLists)
             .HasForeignKey(c => c.userid);

            modelBuilder.Entity<WishList>()
           .HasOne(e => e.products)
           .WithMany()
           .HasForeignKey(p => p.productid);

          
            modelBuilder.Entity<Order>()
          .HasOne(e => e.User)
          .WithMany(u=>u.orders)
          .HasForeignKey(p => p.Userid);

            modelBuilder.Entity<Order>()
         .HasOne(e => e.Products)
         .WithMany()
         .HasForeignKey(p => p.Productid);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User>users { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<CartItem> cartitems { get; set; }
        public DbSet<WishList> wishlists { get; set; }
        public DbSet<Category> categories { get; set; }




    }
}
