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
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 商品种子数据
            modelBuilder.Entity<Toy>().HasData(
      new Toy
      {
          Id = 1,
          Name = "Remote Control Car",
          Description = "Fast and fun",
          LongDescription = "This remote control car offers precise steering and powerful acceleration. Built with a durable body, it's perfect for outdoor racing and high-speed adventures.",
          Price = 59.99M,
          ImageUrl = "rc_car.jpg",
          ClickCount = 0
      },
      new Toy
      {
          Id = 2,
          Name = "Building Blocks",
          Description = "Colorful and creative",
          LongDescription = "A vibrant set of building blocks designed to spark creativity and improve spatial awareness. Perfect for constructing imaginative worlds and fun learning experiences.",
          Price = 39.99M,
          ImageUrl = "blocks.jpg",
          ClickCount = 0
      },
      new Toy
      {
          Id = 3,
          Name = "Teddy Bear",
          Description = "Soft and cuddly",
          LongDescription = "This adorable teddy bear features ultra-soft fur and a huggable design. An ideal bedtime companion to provide comfort and security for young children.",
          Price = 24.99M,
          ImageUrl = "teddy.jpg",
          ClickCount = 0
      },
      new Toy
      {
          Id = 4,
          Name = "Dinosaur Figure",
          Description = "Realistic toy dino",
          LongDescription = "A lifelike dinosaur figure with detailed textures and vivid colors. Great for prehistoric play, educational storytelling, and dino-loving kids.",
          Price = 19.99M,
          ImageUrl = "dino.jpg",
          ClickCount = 0
      },
      new Toy
      {
          Id = 5,
          Name = "Rubik's Cube",
          Description = "Brain teaser puzzle",
          LongDescription = "The classic 3x3 Rubik's Cube offers endless puzzle-solving fun. A timeless toy that sharpens logic, concentration, and memory skills.",
          Price = 14.99M,
          ImageUrl = "cube.jpg",
          ClickCount = 0
      },
      new Toy
      {
          Id = 6,
          Name = "Toy Train Set",
          Description = "Complete railway set",
          LongDescription = "This complete toy train set includes engines, carriages, and tracks. Kids can build their own railway and enjoy hours of imaginative play.",
          Price = 49.99M,
          ImageUrl = "train.jpg",
          ClickCount = 0
      },
      new Toy
      {
          Id = 7,
          Name = "Musical Keyboard",
          Description = "Small piano for kids",
          LongDescription = "A kid-friendly electronic keyboard with multiple tones and rhythms. Helps children develop a love for music and rhythm while having fun.",
          Price = 69.99M,
          ImageUrl = "keyboard.jpg",
          ClickCount = 0
      },
      new Toy
      {
          Id = 8,
          Name = "Puzzle Board",
          Description = "Educational shape puzzle",
          LongDescription = "A wooden puzzle board with various shapes and colors. Ideal for early learners to enhance shape recognition, coordination, and problem-solving skills.",
          Price = 19.99M,
          ImageUrl = "puzzle.jpg",
          ClickCount = 0
      },
      new Toy
      {
          Id = 9,
          Name = "Toy Soldier Set",
          Description = "Army action figures",
          LongDescription = "A detailed set of toy soldiers in different poses, complete with gear and vehicles. Perfect for military-themed play and battlefield re-enactments.",
          Price = 29.99M,
          ImageUrl = "soldiers.jpg",
          ClickCount = 0
      },
      new Toy
      {
          Id = 10,
          Name = "Basketball Hoop",
          Description = "Indoor mini hoop",
          LongDescription = "This indoor mini basketball hoop is easy to mount and comes with a soft ball. A fun way to practice shots and enjoy physical activity indoors.",
          Price = 34.99M,
          ImageUrl = "hoop.jpg",
          ClickCount = 0
      }
  );
        }
    }
}
