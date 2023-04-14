using Microsoft.EntityFrameworkCore;

namespace OrgChamp.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    { }

    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().ToTable("Teams", "Organization");
        modelBuilder.Entity<TeamMember>().ToTable("TeamMembers", "Organization");
        modelBuilder.Entity<User>().ToTable("Users", "UserManagement");
        modelBuilder.Entity<UserDetails>().ToTable("UserDetails", "UserManagement");
    }
}