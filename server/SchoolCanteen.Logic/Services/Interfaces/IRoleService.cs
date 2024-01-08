using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.RoleDTOs;

namespace SchoolCanteen.Logic.Services.Interfaces;

public interface IRoleService
{
    Task<Role> CreateAsync(Role role);
    Task<Role> GetByNameAsync(string roleName, Guid companyId);
    Task<bool> UpdateAsync(Role role);
    Task<bool> DeleteAsync(Guid roleId);
    Task<IEnumerable<SimpleRoleDTO>> GetAllAsync(Guid companyId);
}
