
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.UnitDTOs;

namespace SchoolCanteen.Logic.Services.UnitServices;

public interface IUnitService
{
    Task<Unit> CreateAsync(SimpleUnitDto unit);
    Task<bool> UpdateAsync(SimpleUnitDto unit);
    Task<bool> DeleteAsync(int id);
    Task<Unit> GetByIdAsync(int id);
    Task<Unit> GetByNameAsync(string name);
    Task<IEnumerable<SimpleUnitDto>> GetByCompanyIdAsync(Guid companyId);
    Task<IEnumerable<SimpleUnitDto>> GetAllAsync();
}
