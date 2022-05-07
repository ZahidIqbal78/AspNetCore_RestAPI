using dotnet_webapi_example.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore_RestAPI.Data
{
    public class TestDbContext : IdentityDbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        //DbSets
        #region DbSets
        public DbSet<Product> Products { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e=>e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach(var entityEntry in entries)
            {
                if(entityEntry.Metadata.FindProperty("UpdatedAt") != null)
                {
                    entityEntry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }

                if(entityEntry.Metadata.FindProperty("CreatedAt") != null)
                {
                    if(entityEntry.State == EntityState.Added)
                    {
                        entityEntry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}