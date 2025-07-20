using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToyShopWebApp.Models;

namespace ToyShopWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Toy> Toys { get; set; }
        public DbSet<User> WebUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ToyInventory> ToyInventories { get; set; }
        public DbSet<BrowsingHistory> BrowsingHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 商品种子数据
            modelBuilder.Entity<Toy>().HasData(
                new Toy { Id = 1, Name = "Remote Control Car", Description = "Fast and fun", Price = 59.99M, ImageUrl = "rc_car.jpg" },
                new Toy { Id = 2, Name = "Building Blocks", Description = "Colorful and creative", Price = 39.99M, ImageUrl = "blocks.jpg" },
                new Toy { Id = 3, Name = "Teddy Bear", Description = "Soft and cuddly", Price = 24.99M, ImageUrl = "teddy.jpg" },
                new Toy { Id = 4, Name = "Dinosaur Figure", Description = "Realistic toy dino", Price = 19.99M, ImageUrl = "dino.jpg" },
                new Toy { Id = 5, Name = "Rubik's Cube", Description = "Brain teaser puzzle", Price = 14.99M, ImageUrl = "cube.jpg" },
                new Toy { Id = 6, Name = "Toy Train Set", Description = "Complete railway set", Price = 49.99M, ImageUrl = "train.jpg" },
                new Toy { Id = 7, Name = "Musical Keyboard", Description = "Small piano for kids", Price = 69.99M, ImageUrl = "keyboard.jpg" },
                new Toy { Id = 8, Name = "Puzzle Board", Description = "Educational shape puzzle", Price = 19.99M, ImageUrl = "puzzle.jpg" },
                new Toy { Id = 9, Name = "Toy Soldier Set", Description = "Army action figures", Price = 29.99M, ImageUrl = "soldiers.jpg" },
                new Toy { Id = 10, Name = "Basketball Hoop", Description = "Indoor mini hoop", Price = 34.99M, ImageUrl = "hoop.jpg" }
            );
        }
    }
}
