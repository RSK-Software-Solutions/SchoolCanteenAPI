
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.DATA.Repositories;
using SchoolCanteen.DATA.Repositories.Interfaces;
using SchoolCanteen.Logic.DTOs.RoleDTOs;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services.Interfaces;

namespace SchoolCanteen.Logic.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger logger;

    public RoleService(DatabaseApiContext databaseApiContext, IMapper mapper, ILogger<RoleService> logger)
    {
        _roleRepository = new RoleRepository(databaseApiContext, logger);
        _mapper = mapper;
        this.logger = logger;
    }

    public async Task<Role> CreateAsync(Role newRole)
    {
        var role = await _roleRepository.GetByNameAsync(newRole.RoleName, newRole.CompanyId);
        if (role != null) 
        {
            logger.LogInformation($"Role {newRole.RoleName} already exists.");
            return role;
        }
        await _roleRepository.AddAsync(newRole);
        return newRole;
    }

    public async Task<bool> DeleteAsync(Guid roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null) return false;

        await _roleRepository.DeleteAsync(role);
        return true;
    }

    public async Task<IEnumerable<SimpleRoleDTO>> GetAllAsync(Guid companyId)
    {
        var roles = await _roleRepository.GetAllAsync(companyId);

        return roles.Select(role =>
        {
            var simpleUserDtos = new List<SimpleUserDTO>();

            foreach (var user in role.Users)
            {
                simpleUserDtos.Add(_mapper.Map<SimpleUserDTO>(user));
            }

            return new SimpleRoleDTO(role.RoleId, role.RoleName);
        });
    }

    public async Task<Role> GetByNameAsync(string roleName, Guid companyId)
    {
        var role = await _roleRepository.GetByNameAsync(roleName, companyId);
        if (role == null) return null;

        return role;
    }

    public async Task<bool> UpdateAsync(Role role)
    {
        var existingRole = await _roleRepository.GetByIdAsync(role.RoleId);
        if (existingRole == null) return false;

        return await _roleRepository.UpdateAsync(role);
    }
}
