
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.DatabaseConnector;

public class DatabaseApiContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserDetails> UsersDetails { get; set; }

    public DatabaseApiContext(DbContextOptions<DatabaseApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>().HasKey(c => c.CompanyId);
        modelBuilder.Entity<User>().HasKey(c => c.UserId);
        modelBuilder.Entity<Role>().HasKey(c => c.RoleId);
        //modelBuilder.Entity<Role>().HasData(
        //    new Role { RoleId=Guid.NewGuid(), RoleName = "admin"},
        //    new Role { RoleId=Guid.NewGuid(), RoleName = "logistyk"},
        //    new Role { RoleId = Guid.NewGuid(), RoleName = "kucharz" }
        //    );
        modelBuilder.Entity<UserDetails>().HasKey(c => c.UserDetailsId);

        modelBuilder.Entity<Company>()
            .HasMany(e => e.Users)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .IsRequired();

        modelBuilder.Entity<UserDetails>()
            .HasOne(e => e.User)
            .WithOne(e => e.UserDetails)
            .HasForeignKey<User>(e => e.UserDetailsId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(e => e.Roles)
            .WithMany(e => e.Users)
            .UsingEntity<UserRole>(
                l => l.HasOne<Role>(e =>e.Role)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.RoleForeignId),

                r => r.HasOne<User>(e => e.User)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.UserForeignId)
            );
    }
}
