
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services.Interfaces;
using SchoolCanteen.DATA.Repositories;
using SchoolCanteen.DATA.Repositories.Interfaces;
using System.Data;
using SchoolCanteen.Logic.DTOs.RoleDTOs;

namespace SchoolCanteen.Logic.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> logger;
    private readonly IUserDetailsService _userDetailsService;
    private readonly IUserRepository _userRepository;

    public UserService(DatabaseApiContext databaseApiContext, IMapper mapper, ILogger<UserService> logger,
        IUserDetailsService userDetailsService, IUserRepository userRepository)
    {
        this.logger = logger;
        _mapper = mapper;
        _userRepository = userRepository;
        _userDetailsService = userDetailsService;
    }

    public async Task<SimpleUserDTO> CreateAsync(CreateUserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.Roles.Add(userDto.Role);

        await _userRepository.AddAsync(user);

        var userDetails = await CreateEmptyUserDetailsRecord(user);

        return _mapper.Map<SimpleUserDTO>(user);
    }

    public async Task<IEnumerable<SimpleUserDTO>> GetAllAsync(Guid companyId)
    {
        var users = await _userRepository.GetAllAsync(companyId);

        //return users.Select(user => _mapper.Map<SimpleUserDTO>(user.Roles.Select(role => _mapper.Map<SimpleRoleDTO>(role))));
        return users.Select(user => 
        {
            var simpleRoleDtos = new List<SimpleRoleDTO>();

            foreach (var role in user.Roles)
            {
                simpleRoleDtos.Add(_mapper.Map<SimpleRoleDTO>(role));
            }

            return new SimpleUserDTO
            {
                UserId = user.UserId,
                Login = user.Login,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                Roles = simpleRoleDtos
            };
        });
    }

    public async Task<SimpleUserDTO> GetByNameAsync(string userLogin, Guid companyId)
    {
        var user = await _userRepository.GetByNameAsync(userLogin, companyId);
        if (user == null) return null;

        return _mapper.Map<SimpleUserDTO>(user);
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var user = await _userRepository.GetByIdAsync(Id);
        if (user == null) return false;

        await _userRepository.DeleteAsync(user);
        return true;
    }

    public async Task<bool> UpdateAsync(EditUserDTO userDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(userDto.UserId);
        if (existingUser == null) return false;

        _mapper.Map(userDto, existingUser);

        return await _userRepository.UpdateAsync(existingUser);
    }

    private async Task<UserDetails> CreateEmptyUserDetailsRecord(User user)
    {
        var userDetails = await _userDetailsService.CreateAsync(new UserDetails { User = user });
        return userDetails;
    }
}
