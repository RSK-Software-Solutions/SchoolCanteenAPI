
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Services.Repositories;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllAsync(Guid companyId);
    Task<Role> GetByNameAsync(string roleName, Guid companyId);
    Task<Role> GetByIdAsync(Guid id);
    Task<bool> AddAsync(Role role);
    Task<bool> DeleteAsync(Role role);
    Task<bool> UpdateAsync(Role role);
}
