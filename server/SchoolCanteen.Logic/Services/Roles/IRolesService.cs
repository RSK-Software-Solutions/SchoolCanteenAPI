
using Microsoft.AspNetCore.Identity;

namespace SchoolCanteen.Logic.Services.Roles;

public interface IRolesService
{
    Task<IdentityResult> CreateRoleAsync(string roleName);
}
