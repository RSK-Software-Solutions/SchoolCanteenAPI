
using Microsoft.EntityFrameworkCore;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.DatabaseConnector;

public static class ModelBuilderExtentions
{
    public static void ConfigureCompany(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>()
            .HasKey(c => c.CompanyId);

        modelBuilder.Entity<Company>()
            .HasMany(c => c.Users)
            .WithOne(u => u.Company)
            .HasForeignKey(u => u.CompanyId)
            .IsRequired();

        modelBuilder.Entity<Company>()
            .HasMany(c => c.Roles)
            .WithOne(r => r.Company)
            .HasForeignKey(r => r.CompanyId);
    }

    public static void ConfigureUser(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(c => c.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.UserDetails)
            .WithOne(d => d.User)
            .HasForeignKey<UserDetails>(d => d.UserId)
            .IsRequired();

        //modelBuilder.Entity<User>()
        //    .HasMany(u => u.Roles)
        //    .WithMany(r => r.Users)
        //    .UsingEntity(ur => ur.ToTable("UsersToRoles"));

        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRole>(
                u => u.HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleForeignId),

                r => r.HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserForeignId),

                ur => ur.HasKey(x => new { x.RoleForeignId, x.UserForeignId })

                );
    }

    public static void ConfigureRole(this ModelBuilder modelBuilder) 
    {
        modelBuilder.Entity<Role>()
            .HasKey(c => c.RoleId);
    }

    public static void ConfigureUserDetails(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDetails>()
            .HasKey(c => c.UserDetailsId);
    }

    //public static void ConfigureUserRole(this ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<UserRole>()
    //        .HasKey(key => new { key.UserForeignId, key.RoleForeignId });
    //}
}
