
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.UnitRepo;

internal class UnitRepository : IUnitRepository
{
    public Task<bool> AddAsync(Unit unit)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Unit unit)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Unit>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Unit> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Unit> GetByNameAsync(string unitName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Unit unit)
    {
        throw new NotImplementedException();
    }
}
