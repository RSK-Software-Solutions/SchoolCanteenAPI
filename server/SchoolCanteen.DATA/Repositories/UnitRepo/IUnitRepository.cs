
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.UnitRepo;

public interface IUnitRepository
{
    Task<IEnumerable<Unit>> GetAllAsync(Guid companyId);
    Task<Unit> GetByIdAsync(int id);
    Task<Unit> GetByNameAsync(string name, Guid companyId);
    Task<bool> AddAsync(Unit unit);
    Task<bool> DeleteAsync(Unit unit);

}
