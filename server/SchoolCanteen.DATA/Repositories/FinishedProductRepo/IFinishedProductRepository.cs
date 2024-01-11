
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.FinishedProductRepo;

public interface IFinishedProductRepository
{
    Task<IEnumerable<FinishedProduct>> GetAllAsync();
    Task<FinishedProduct> GetByNameAsync(string finishedProductName);
    Task<FinishedProduct> GetByIdAsync(int id);
    Task<bool> AddAsync(FinishedProduct finishedProduct);
    Task<bool> DeleteAsync(FinishedProduct finishedProduct);
    Task<bool> UpdateAsync(FinishedProduct finishedProduct);
}
