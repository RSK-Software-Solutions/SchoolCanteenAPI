
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.ProductRepo;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(Guid companyId);
    Task<Product> GetByNameAsync(string productName);
    Task<Product> GetByIdAsync(int id);
    Task<bool> AddAsync(Product product);
    Task<bool> DeleteAsync(Product product);
    Task<bool> UpdateAsync(Product product);
}
