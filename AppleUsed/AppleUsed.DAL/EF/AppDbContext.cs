using AppleUsed.DAL.EF;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AppleUsed.DAL.Identity
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
           CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdPhotos> AdPhotos { get; set; }
        public DbSet<Characteristics> Characteristics { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        public DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<ProductModels> ProductModels { get; set; }
        public DbSet<ProductStates> ProductStates { get; set; }
        public DbSet<ProductMemories> ProductMemories { get; set; }
        public DbSet<CityArea> CityAreas { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Services> Services { get; set; }

    }
}
