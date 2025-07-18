using Microsoft.EntityFrameworkCore;
using ToyShopWebApp.Models;

namespace ToyShopWebApp.Data
{
    public class ToyShopContext : DbContext
    {
        public ToyShopContext(DbContextOptions<ToyShopContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Toy> Toys { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<BrowsingHistory> BrowsingHistories { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ToyInventory> ToyInventories { get; set; }
    }
}
