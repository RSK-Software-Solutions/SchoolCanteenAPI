
using Microsoft.AspNetCore.Identity;
using SchoolCanteen.Logic.DTOs.RoleDTOs;

namespace SchoolCanteen.Logic.Services.Roles;

public interface IRolesService
{
    Task<IEnumerable<SimpleRoleDTO>> GetRolesAsync();
}
