
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Services;

public interface IRoleService
{
    Task<Role> CreateAsync(Role role);
    Task<Role> GetByNameAsync(string roleName, Guid companyId);
    Task<bool> UpdateAsync(Role role);
    Task<bool> DeleteAsync(Guid roleId);
    Task<IEnumerable<Role>> GetAllAsync(Guid companyId);
}
