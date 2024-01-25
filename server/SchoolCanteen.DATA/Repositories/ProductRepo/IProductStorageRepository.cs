
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.ProductRepo;

public interface IProductStorageRepository
{
    Task<IEnumerable<ProductStorage>> GetAllAsync(Guid companyId);
    Task<IEnumerable<ProductStorage>> GetAllByFinishProductId(Guid companyId, FinishedProduct finishedProduct);
    Task<ProductStorage> GetByIdAsync(Guid companyId, int productStorageId);
    Task<bool> AddAsync(ProductStorage productStorage);
    Task<bool> DeleteAsync(ProductStorage productStorage);
    Task<bool> UpdateAsync(ProductStorage productStorage);
}
