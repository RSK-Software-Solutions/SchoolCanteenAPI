
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.FinishedProductRepo;

public interface IFinishedProductRepository
{
    Task<IEnumerable<FinishedProduct>> GetAllAsync(Guid companyId);
    Task<FinishedProduct> GetByNameAsync(string finishedProductName, Guid companyId);
    Task<FinishedProduct> GetByIdAsync(int id, Guid companyId);
    Task<bool> AddAsync(FinishedProduct finishedProduct);
    Task<bool> DeleteAsync(FinishedProduct finishedProduct);
    Task<bool> UpdateAsync(FinishedProduct finishedProduct);
}
