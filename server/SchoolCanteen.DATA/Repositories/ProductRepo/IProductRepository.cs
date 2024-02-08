
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.ProductRepo;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(Guid companyId);
    Task<IEnumerable<Product>> GetListByNameAsync(string productName, Guid companyId);
    Task<Product> GetByNameAsync(string productName, Guid companyId);
    Task<Product> GetByIdAsync(int id, Guid companyId);
    Task<bool> AddAsync(Product product);
    Task<bool> DeleteAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<int> CountAsync(Guid companyId);
    Task<int> CountLowAsync(Guid companyId);
}
