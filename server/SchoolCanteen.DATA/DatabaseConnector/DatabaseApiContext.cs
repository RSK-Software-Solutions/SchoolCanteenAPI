
using Microsoft.EntityFrameworkCore;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.DatabaseConnector;

public class DatabaseApiContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UsersRoles { get; set; }
    public DbSet<UserDetails> UsersDetails { get; set; }

    public DatabaseApiContext(DbContextOptions<DatabaseApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureCompany();
        modelBuilder.ConfigureUser();
        modelBuilder.ConfigureRole();
        modelBuilder.ConfigureUserDetails();
    }
}
