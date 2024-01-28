
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.ProductStorageRepo;

public class ProductStorageRepository : IProductStorageRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger logger;

    public ProductStorageRepository(DatabaseApiContext ctx, ILogger<ProductStorageRepository> logger)
    {
        this.ctx = ctx;
        this.logger = logger;
    }

    public async  Task<bool> DeleteAsync(ProductStorage productStorage)
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
}
