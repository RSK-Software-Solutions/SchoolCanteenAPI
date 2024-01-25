
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.FinishedProductRepo;

public class ProductFinishedProductRepository : IProductFinishedProductRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger logger;

    public ProductFinishedProductRepository(DatabaseApiContext ctx, ILogger<ProductFinishedProductRepository> logger)
    {
        this.logger = logger;
        this.ctx = ctx;
    }

    public async Task<bool> AddAsync(ProductFinishedProduct productFinishedProduct)
    {
        try
        {
            await ctx.AddAsync(productFinishedProduct);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public Task<bool> DeleteAsync(ProductFinishedProduct productFinishedProduct)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetByFinishedProductIdAsync(int finishedProductId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FinishedProduct>> GetByProdutIdAsync(int productId)
    {
        throw new NotImplementedException();
    }
}
