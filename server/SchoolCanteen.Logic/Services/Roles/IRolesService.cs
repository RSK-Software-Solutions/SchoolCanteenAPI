
using Microsoft.AspNetCore.Identity;
using SchoolCanteen.Logic.DTOs.RoleDTOs;

namespace SchoolCanteen.Logic.Services.Roles;

public interface IRolesService
{
    Task<IdentityResult> CreateRoleAsync(string roleName);
    Task<IEnumerable<SimpleRoleDTO>> GetRolesAsync();
}
