
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.Logic.DTOs.RoleDTOs;
using SchoolCanteen.Logic.Services.Authentication;

namespace SchoolCanteen.Logic.Services.Roles;

public class RolesService : IRolesService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AuthService> logger;
    private readonly IMapper mapper;

    public RolesService(RoleManager<IdentityRole> roleManager, ILogger<AuthService> logger, IMapper mapper)
    {
        _roleManager = roleManager;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<IdentityResult> CreateRoleAsync(string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (roleExists) return IdentityResult.Failed(new IdentityError { Description = $"Role {roleName} already exists"} );

        return await _roleManager.CreateAsync(new IdentityRole(roleName));
    }

    public async Task<IEnumerable<SimpleRoleDTO>> GetRolesAsync()
    {
        var roles = await _roleManager.Roles
            .Where(x => x.Name != "SuperAdmin")
            .OrderBy(x => x.Name)
            .ToListAsync();

        return roles.Select(role => mapper.Map<SimpleRoleDTO>(role));
    }
}
