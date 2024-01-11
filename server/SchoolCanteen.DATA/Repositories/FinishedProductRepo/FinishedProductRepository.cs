
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.FinishedProductRepo;

public class FinishedProductRepository : IFinishedProductRepository
{
    public Task<bool> AddAsync(FinishedProduct finishedProduct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(FinishedProduct finishedProduct)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FinishedProduct>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<FinishedProduct> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<FinishedProduct> GetByNameAsync(string finishedProductName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(FinishedProduct finishedProduct)
    {
        throw new NotImplementedException();
    }
}
