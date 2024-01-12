﻿
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
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<Company>()
            .HasMany(c => c.Users)
            .WithOne(u => u.Company)
            .HasForeignKey(u => u.CompanyId)
            .IsRequired();

        modelBuilder.Entity<Company>()
            .HasMany(c => c.Recipes)
            .WithOne(r => r.Company)
            .HasForeignKey(r => r.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public static void ConfigureUnit(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Unit>()
            .HasKey(c => c.UnitId);
    }

    public static void ConfigureProduct(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(c => c.ProductId);

        modelBuilder.Entity<Unit>()
            .HasOne(e => e.Product)
            .WithOne(e => e.Unit)
            .HasForeignKey<Product>(d => d.UnitId)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .HasMany(e => e.FinishedProducts)
            .WithMany(e => e.Products)
            .UsingEntity(
                "ProductFinishedProduct",
                l => l.HasOne(typeof(FinishedProduct)).WithMany().HasForeignKey("FinishedProductId").HasPrincipalKey(nameof(FinishedProduct.FinishedProductId)),
                r => r.HasOne(typeof(Product)).WithMany().HasForeignKey("ProductId").HasPrincipalKey(nameof(Product.ProductId)),
                j => j.HasKey("ProductId", "FinishedProductId"));
    }

    public static void ConfigureFinishedProduct(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FinishedProduct>().HasKey(c => c.FinishedProductId);

    }

    public static void ConfigureRecipe(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>().HasKey(c => c.RecipeId);

        modelBuilder.Entity<Recipe>()
            .HasMany(d => d.Details)
            .WithOne(r => r.Recipe)
            .HasForeignKey(d => d.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public static void ConfigureRecipeDetail(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeDetail>().HasKey(c => c.RecipeDetailId);

        modelBuilder.Entity<RecipeDetail>()
            .HasOne(p => p.Product)
            .WithMany(d => d.RecipeDetails)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

//public static void ConfigureUserDetails(this ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<UserDetails>()
//        .HasKey(c => c.UserDetailsId);
//}

//public static void ConfigureUserRole(this ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<UserRole>()
//        .HasKey(key => new { key.UserForeignId, key.RoleForeignId });
//}


//modelBuilder.Entity<User>()
//    .HasMany(u => u.Roles)
//    .WithMany(r => r.Users)
//    .UsingEntity(ur => ur.ToTable("UsersToRoles"));

//modelBuilder.Entity<User>()
//    .HasMany(u => u.Roles)
//    .WithMany(r => r.Users)
//    .UsingEntity<UserRole>(
//        u => u.HasOne(ur => ur.Role)
//        .WithMany()
//        .HasForeignKey(ur => ur.RoleForeignId),

//        r => r.HasOne(ur => ur.User)
//        .WithMany()
//        .HasForeignKey(ur => ur.UserForeignId),

//        ur => ur.HasKey(x => new { x.RoleForeignId, x.UserForeignId })

//        );