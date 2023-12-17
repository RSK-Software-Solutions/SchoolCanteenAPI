
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services.Interfaces;
using SchoolCanteen.DATA.Repositories;
using SchoolCanteen.DATA.Repositories.Interfaces;
using System.Data;

namespace SchoolCanteen.Logic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger logger;

    public UserService(DatabaseApiContext databaseApiContext, IMapper mapper, ILogger<UserService> logger)
    {
        this.logger = logger;
        _userRepository = new UserRepository(databaseApiContext, logger);
        _mapper = mapper;
    }

    public async Task<SimpleUserDTO> CreateAsync(CreateUserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userRepository.AddAsync(user);

        return _mapper.Map<SimpleUserDTO>(user);
    }

    public async Task<IEnumerable<SimpleUserDTO>> GetAllAsync(Guid companyId)
    {
        var users = await _userRepository.GetAllAsync(companyId);

        return users.Select(user => _mapper.Map<SimpleUserDTO>(user));
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
}
