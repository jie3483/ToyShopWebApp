using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToyShopWebApp.Models;

namespace ToyShopWebApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Toy> Toys { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<ToyInventory> ToyInventories { get; set; }
    public DbSet<BrowsingHistory> BrowsingHistories { get; set; }

}
