﻿
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

    public async Task<int> CountAsync(Guid companyId)
    {
        return await ctx.Products
            .Where(e => e.CompanyId == companyId)
            .CountAsync();
    }

    public async Task<int> CountLowAsync(Guid companyId)
    {
        return await ctx.Products
            .Where(e => e.CompanyId == companyId && e.Quantity < e.MinQuantity)
            .CountAsync();
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
    public async Task<IEnumerable<Product>> GetAllAsync(Guid companyId)
    {
        try
        {
            return await ctx.Products
                .Where(e => e.CompanyId == companyId)
                .Include(u => u.Unit)
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
    public async Task<Product> GetByIdAsync(int id, Guid companyId)
    {
        try
        {
            return await ctx.Products
                .Include(u => u.Unit)
                .FirstOrDefaultAsync(e => e.ProductId == id && e.CompanyId == companyId);
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
    public async Task<Product> GetByNameAsync(string productName, Guid companyId)
    {
        try
        {
            return await ctx.Products.FirstOrDefaultAsync(e => e.Name == productName && e.CompanyId == companyId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<IEnumerable<Product>> GetListByNameAsync(string productName, Guid companyId)
    {
        try
        {
            return await ctx.Products
                .Where(e => e.CompanyId == companyId && e.Name.Contains(productName))
                .Include(u => u.Unit)
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
