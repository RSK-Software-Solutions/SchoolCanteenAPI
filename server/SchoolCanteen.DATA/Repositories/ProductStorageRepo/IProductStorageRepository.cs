
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.ProductStorageRepo;

public interface IProductStorageRepository
{
    Task<bool> DeleteAsync(ProductStorage productStorage);
}

