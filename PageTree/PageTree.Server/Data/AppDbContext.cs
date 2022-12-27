using Corelibs.Basic.Repository;
using Microsoft.EntityFrameworkCore;

namespace PageTree.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectUserList> ProjectUserLists { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Signature> Signatures { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.SetPrimaryKey<User>();
            mb.SetPrimaryKey<ProjectUserList>();
            mb.SetPrimaryKey<Project>();
            mb.SetPrimaryKey<Page>();
            mb.SetPrimaryKey<Signature>();
        }
    }

    public class User : JsonEntity {}
    public class ProjectUserList : JsonEntity {}
    public class Project : JsonEntity {}
    public class Page : JsonEntity {}
    public class Signature : JsonEntity {}

    static class ModelBuilderExtensions
    {
        public static void SetPrimaryKey<TEntity>(this ModelBuilder modelBuilder) where TEntity : JsonEntity
        {
            modelBuilder.Entity<User>()
                .HasKey(k => new { k.ID })
                .HasName("PK_Guid");
        }
    }
}
