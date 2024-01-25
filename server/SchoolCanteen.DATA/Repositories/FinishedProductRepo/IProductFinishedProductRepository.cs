
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.FinishedProductRepo;

public interface IProductFinishedProductRepository
{
    Task<IEnumerable<FinishedProduct>> GetByProdutIdAsync(int productId);
    Task<IEnumerable<Product>> GetByFinishedProductIdAsync(int finishedProductId);
    Task<bool> AddAsync(ProductFinishedProduct productFinishedProduct);
    Task<bool> DeleteAsync(ProductFinishedProduct productFinishedProduct);
}
