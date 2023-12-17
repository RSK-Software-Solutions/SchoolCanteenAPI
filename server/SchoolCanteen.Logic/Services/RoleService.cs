
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.Logic.Services.Repositories;
using SchoolCanteen.Logic.Services.Repositories.Interfaces;

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

    public async Task<Role> CreateAsync(Role role)
    {
        var existRole = await _roleRepository.GetByNameAsync(role.RoleName, role.CompanyId);
        if (existRole != null) 
        {
            logger.LogInformation($"Role {role.RoleName} already exists.");
            return existRole;
        }
        await _roleRepository.AddAsync(role);
        return role;
    }

    public async Task<bool> DeleteAsync(Guid roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null) return false;

        await _roleRepository.DeleteAsync(role);
        return true;
    }

    public async Task<IEnumerable<Role>> GetAllAsync(Guid companyId)
    {
        return await _roleRepository.GetAllAsync(companyId);
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
