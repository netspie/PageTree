using Corelibs.Basic.Repository;
using Microsoft.EntityFrameworkCore;

namespace PageTree.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(k => new { k.ID })
                .HasName("PK_Guid");
        }
    }

    public class User : JsonEntity {}
}
