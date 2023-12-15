
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Services;

public interface IRoleService
{
    Task<Role> CreateAsync(Role role);
    Task<Role> GetByNameAsync(string roleName);
    Task<bool> UpdateAsync(Role role);
    Task DeleteAsync(Guid roleId);
    Task<IEnumerable<Role>> GetAllAsync();
}
