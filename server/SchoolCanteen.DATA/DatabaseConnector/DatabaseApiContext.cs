
using Microsoft.EntityFrameworkCore;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.DatabaseConnector;

public class DatabaseApiContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductStorage> ProductStorages { get; set; }
    public DbSet<FinishedProduct> FinishedProducts { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeDetail> RecipeDetails { get; set; }

    public DatabaseApiContext(DbContextOptions<DatabaseApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureCompany();
        modelBuilder.ConfigureProduct();
        modelBuilder.ConfigureUnit();
        modelBuilder.ConfigureFinishedProduct();
        modelBuilder.ConfigureRecipeDetail();
        modelBuilder.ConfigureRecipe();
    }
}
