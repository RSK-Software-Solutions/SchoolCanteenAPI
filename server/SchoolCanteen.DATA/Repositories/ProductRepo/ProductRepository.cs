
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.ProductRepo;

public class ProductRepository : IProductRepository
{
    public Task<bool> AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByNameAsync(string productName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }
}
