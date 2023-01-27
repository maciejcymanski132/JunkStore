using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JunkStore.Models;

namespace JunkStore.Data
{
    public class ApplicationDbContext :  IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<JunkStore.Models.Product> Product { get; set; }
        public DbSet<JunkStore.Models.Customer> Customer { get; set; }
        public DbSet<JunkStore.Models.Order> Order { get; set; }
        public DbSet<JunkStore.Models.Shipping> Shipping { get; set; }
        public DbSet<JunkStore.Models.Payment> Payment { get; set; }
        public DbSet<JunkStore.Models.ProductType> ProductType { get; set; }
    }
}