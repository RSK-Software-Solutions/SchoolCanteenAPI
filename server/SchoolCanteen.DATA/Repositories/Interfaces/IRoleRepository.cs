using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllAsync(Guid companyId);
    Task<Role> GetByNameAsync(string roleName, Guid companyId);
    Task<Role> GetByIdAsync(Guid id);
    Task<bool> AddAsync(Role newRole);
    Task<bool> DeleteAsync(Role role);
    Task<bool> UpdateAsync(Role role);
}
