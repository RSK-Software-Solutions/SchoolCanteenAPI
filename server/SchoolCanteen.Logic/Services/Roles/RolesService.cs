
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolCanteen.Logic.Services.Authentication;

namespace SchoolCanteen.Logic.Services.Roles;

public class RolesService : IRolesService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AuthService> logger;

    public RolesService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> CreateRoleAsync(string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (roleExists) return IdentityResult.Failed(new IdentityError { Description = $"Role {roleName} already exists"} );

        return await _roleManager.CreateAsync(new IdentityRole(roleName));
    }
}
