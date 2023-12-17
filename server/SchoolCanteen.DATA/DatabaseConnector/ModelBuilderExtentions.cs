
using Microsoft.EntityFrameworkCore;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.DatabaseConnector;

public static class ModelBuilderExtentions
{
    public static void ConfigureCompany(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().HasKey(c => c.CompanyId);

        modelBuilder.Entity<Company>()
            .HasMany(e => e.Users)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .IsRequired();
    }

    public static void ConfigureUser(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(c => c.UserId);

        modelBuilder.Entity<UserDetails>()
            .HasOne(e => e.User)
            .WithOne(e => e.UserDetails)
            .HasForeignKey<User>(e => e.UserDetailsId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(e => e.Roles)
            .WithMany(e => e.Users)
            .UsingEntity<UserRole>(
                l => l.HasOne<Role>(e => e.Role)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.RoleForeignId),

                r => r.HasOne<User>(e => e.User)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.UserForeignId)
                );
    }

    public static void ConfigureRole(this ModelBuilder modelBuilder) 
    {
        modelBuilder.Entity<Role>().HasKey(c => c.RoleId);
        //modelBuilder.Entity<Role>().HasData(
        //    new Role { RoleId=Guid.NewGuid(), RoleName = "admin"},
        //    new Role { RoleId=Guid.NewGuid(), RoleName = "logistyk"},
        //    new Role { RoleId = Guid.NewGuid(), RoleName = "kucharz" }
        //    );
    }

    public static void ConfigureUserDetails(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDetails>().HasKey(c => c.UserDetailsId);
    }
}
