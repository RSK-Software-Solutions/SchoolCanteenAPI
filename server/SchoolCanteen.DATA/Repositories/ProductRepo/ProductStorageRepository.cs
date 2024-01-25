
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.ProductRepo;

public class ProductStorageRepository : IProductStorageRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger<ProductRepository> logger;

    public ProductStorageRepository(DatabaseApiContext ctx, ILogger<ProductRepository> logger)
    {
        this.ctx = ctx;
        this.logger = logger;
    }

    public async Task<bool> AddAsync(ProductStorage productStorage)
    {
        try
        {
            await ctx.AddAsync(productStorage);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<bool> DeleteAsync(ProductStorage productStorage)
    {
        try
        {
            ctx.ProductStorages.Remove(productStorage);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<IEnumerable<ProductStorage>> GetAllAsync(Guid companyId)
    {
        try
        {
            return await ctx.ProductStorages
                .Where(e => e.CompanyId == companyId)
                .OrderBy(e => e.Product.Name)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<IEnumerable<ProductStorage>> GetAllByFinishProductId(Guid companyId, FinishedProduct finishedProduct)
    {
        try
        {
            return await ctx.ProductStorages
                .Where(e => e.CompanyId == companyId && e.FinishedProducts.Contains(finishedProduct))
                .OrderBy(e => e.Product.Name)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<ProductStorage> GetByIdAsync(Guid companyId, int productStorageId)
    {
        try
        {
            return await ctx.ProductStorages
                .FirstOrDefaultAsync(e => e.CompanyId == companyId && e.ProductStorageId == productStorageId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public Task<bool> UpdateAsync(ProductStorage productStorage)
    {
        throw new NotImplementedException();
    }
}
