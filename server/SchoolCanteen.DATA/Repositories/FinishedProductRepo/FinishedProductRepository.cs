
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.ProductRepo;

namespace SchoolCanteen.DATA.Repositories.FinishedProductRepo;

public class FinishedProductRepository : IFinishedProductRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger<FinishedProductRepository> logger;

    public FinishedProductRepository(DatabaseApiContext ctx, ILogger<FinishedProductRepository> logger)
    {
        this.ctx = ctx;
        this.logger = logger;
    }
    /// <summary>
    /// Asynchronously adds a new FinishedProduct to the database.
    /// </summary>
    /// <param name="finishedProduct"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> AddAsync(FinishedProduct finishedProduct)
    {
        try
        {
            await ctx.AddAsync(finishedProduct);
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
    /// 
    /// </summary>
    /// <param name="finishedProduct"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> DeleteAsync(FinishedProduct finishedProduct)
    {
        try
        {
            ctx.FinishedProducts.Remove(finishedProduct);
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
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IEnumerable<FinishedProduct>> GetAllAsync(Guid companyId)
    {
        try
        {
            return await ctx.FinishedProducts
                .Where(e => e.CompanyId == companyId)
                .Include(x => x.Products)
                .OrderBy(e => e.Name)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<FinishedProduct> GetByIdAsync(int id, Guid companyId)
    {
        try
        {
            return await ctx.FinishedProducts
                .Include(x => x.Products)
                .Where(x => x.CompanyId == companyId)
                .FirstOrDefaultAsync(e => e.FinishedProductId == id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
    /// <summary>
    /// Asynchronously retrieves a   from the database based on its name.
    /// </summary>
    /// <param name="finishedProductName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<FinishedProduct> GetByNameAsync(string finishedProductName, Guid companyId)
    {
        try
        {
            return await ctx.FinishedProducts
                .Include(x => x.Products)
                .Where(x => x.CompanyId == companyId)
                .FirstOrDefaultAsync(e => e.Name == finishedProductName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="finishedProduct"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(FinishedProduct finishedProduct)
    {
        try
        {
            ctx.FinishedProducts.Update(finishedProduct);
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
