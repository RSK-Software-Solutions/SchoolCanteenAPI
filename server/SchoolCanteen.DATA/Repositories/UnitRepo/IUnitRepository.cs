
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.UnitRepo;

public interface IUnitRepository
{
    Task<IEnumerable<Unit>> GetAllAsync();
    Task<Unit> GetByIdAsync(int id);
    Task<bool> AddAsync(Unit unit);
    Task<bool> DeleteAsync(Unit unit);

}
