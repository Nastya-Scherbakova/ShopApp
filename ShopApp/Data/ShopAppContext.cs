using Microsoft.EntityFrameworkCore;
using ShopApp.Models;

namespace ShopApp.Data
{
    public class ShopAppContext : DbContext
    {
        public ShopAppContext(DbContextOptions<ShopAppContext> options)
            : base(options)
        { }
        public DbSet<Item> Items { get; set; }
        public DbSet<PriceInfo> PriceInfos { get; set; }
    }
}
