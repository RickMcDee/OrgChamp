using Microsoft.EntityFrameworkCore;

namespace OrgChamp.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    { }
   
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users", "UserManagement");
        modelBuilder.Entity<UserDetails>().ToTable("UserDetails", "UserManagement");
    }
}