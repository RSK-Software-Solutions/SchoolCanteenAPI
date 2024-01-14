
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.RecipeRepo;

namespace SchoolCanteen.DATA.Repositories.ProductRepo;

public class ProductRepository : IProductRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger<ProductRepository> logger;

    public ProductRepository(DatabaseApiContext ctx, ILogger<ProductRepository> logger)
    {
        this.ctx = ctx;
        this.logger = logger;
    }
    /// <summary>
    /// Asynchronously adds a new Product to the database.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> AddAsync(Product product)
    {
        try
        {
            await ctx.AddAsync(product);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
    /// <summary>
    /// Asynchronously deletes a Product from the database.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> DeleteAsync(Product product)
    {
        try
        {
            ctx.Products.Remove(product);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
    /// <summary>
    /// Asynchronously retrieves all Products objects from the database.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            return await ctx.Products
                .OrderBy(e => e.ProductId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
    /// <summary>
    /// Asynchronously retrieves a Product object from the database based on its identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Product> GetByIdAsync(int id)
    {
        try
        {
            return await ctx.Products.FirstOrDefaultAsync(e => e.ProductId == id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
    /// <summary>
    /// Asynchronously retrieves a Product from the database based on its name.
    /// </summary>
    /// <param name="productName">The name of the Product to retrieve.</param>
    /// <returns></returns>
    /// <exception cref="Exception">Throws an exception with details if there is an issue during the retrieval process.</exception>
    public async Task<Product> GetByNameAsync(string productName)
    {
        try
        {
            return await ctx.Products.FirstOrDefaultAsync(e => e.Name == productName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
    /// <summary>
    /// Asynchronously updates a Product in the database.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(Product product)
    {
        try
        {
            ctx.Products.Update(product);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
