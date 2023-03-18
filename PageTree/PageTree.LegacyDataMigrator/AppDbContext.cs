using Corelibs.Basic.Repository;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ProjectUserList> ProjectUserLists { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<PageTemplate> PageTemplates { get; set; }
    public DbSet<Signature> Signatures { get; set; }
    public DbSet<PracticeCategory> PracticeCategories { get; set; }
    public DbSet<PracticeTactic> PracticeTactics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
          "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PageTreeDatabase"
        );
    }
}

public class User : JsonEntity { }
public class ProjectUserList : JsonEntity { }
public class Project : JsonEntity { }
public class Page : JsonEntity { }
public class PageTemplate : JsonEntity { }
public class Signature : JsonEntity { }
public class PracticeCategory : JsonEntity { }
public class PracticeTactic : JsonEntity { }
