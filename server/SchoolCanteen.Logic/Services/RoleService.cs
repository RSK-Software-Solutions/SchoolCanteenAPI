
using AutoMapper;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services.Repositories;

namespace SchoolCanteen.Logic.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public RoleService(DatabaseApiContext databaseApiContext, IMapper mapper)
    {
        _roleRepository = new RoleRepository(databaseApiContext);
        _mapper = mapper;
    }
    public async Task<Role> CreateAsync(Role role)
    {
        await _roleRepository.AddAsync(role);

        return role;
    }

    public async Task DeleteAsync(Guid roleId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Role> GetByNameAsync(string roleName)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Role role)
    {
        throw new NotImplementedException();
    }
}
