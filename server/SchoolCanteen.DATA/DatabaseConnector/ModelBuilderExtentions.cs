
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

        modelBuilder.Entity<Company>()
            .HasMany(p => p.Products)
            .WithOne(c => c.Company)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Company>()
            .HasMany(p => p.ProductsStorage)
            .WithOne(c => c.Company)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Company>()
            .HasMany(p => p.FinishedProducts)
            .WithOne(c => c.Company)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Company>()
            .HasMany(u => u.Units)
            .WithOne(c => c.Company)
            .HasForeignKey(u => u.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public static void ConfigureUnit(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Unit>()
            .HasKey(c => c.UnitId);


        modelBuilder.Entity<Unit>()
            .HasMany(u => u.Products)
            .WithOne(p => p.Unit)
            .HasForeignKey(p => p.UnitId)
            .IsRequired();

        modelBuilder.Entity<Unit>()
            .HasMany(u => u.RecipeDetails)
            .WithOne(r => r.Unit)
            .HasForeignKey(r => r.RecipeDetailId)
            .IsRequired();
    }

    public static void ConfigureProduct(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(c => c.ProductId);

        //modelBuilder.Entity<Product>()
        //    .HasMany(e => e.FinishedProducts)
        //    .WithMany(e => e.Product)
        //    .UsingEntity(
        //        "ProductFinishedProduct",
        //        l => l.HasOne(typeof(FinishedProduct)).WithMany().HasForeignKey("FinishedProductId").HasPrincipalKey(nameof(FinishedProduct.FinishedProductId)),
        //        r => r.HasOne(typeof(Product)).WithMany().HasForeignKey("ProductId").HasPrincipalKey(nameof(Product.ProductId)),
        //        j => j.HasKey("ProductId", "FinishedProductId"));

    }

    public static void ConfigureProductStorage(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductStorage>().HasKey(c => c.ProductId);

        modelBuilder.Entity<ProductStorage>()
            .HasMany(e => e.FinishedProducts)
            .WithMany(e => e.ProductStorages)
            .UsingEntity(
                "ProductStorageFinishedProduct",
                l => l.HasOne(typeof(FinishedProduct)).WithMany().HasForeignKey("FinishedProductId").HasPrincipalKey(nameof(FinishedProduct.FinishedProductId)),
                r => r.HasOne(typeof(ProductStorage)).WithMany().HasForeignKey("ProductStorageId").HasPrincipalKey(nameof(ProductStorage.ProductStorageId)),
                j => j.HasKey("ProductStorageId", "FinishedProductId"));

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
        modelBuilder.Entity<RecipeDetail>()
            .Property(c => c.RecipeDetailId)
            .UseMySqlIdentityColumn();

        modelBuilder.Entity<RecipeDetail>()
            .HasKey(c => c.RecipeDetailId);

        modelBuilder.Entity<RecipeDetail>()
            .HasOne(p => p.Product)
            .WithMany(d => d.RecipeDetails)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeDetail>()
            .HasOne(u => u.Unit)
            .WithMany(d => d.RecipeDetails)
            .HasForeignKey(u => u.UnitId)
            .OnDelete(DeleteBehavior.NoAction);
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