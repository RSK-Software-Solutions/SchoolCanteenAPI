
using AutoMapper;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services.Repositories;

namespace SchoolCanteen.Logic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(DatabaseApiContext databaseApiContext, IMapper mapper)
    {
        _userRepository = new UserRepository(databaseApiContext);
        _mapper = mapper;
    }

    public async Task<SimpleUserDTO> CreateUserAsync(CreateUserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _userRepository.AddAsync(user);

        return _mapper.Map<SimpleUserDTO>(user);
    }

    public Task<IEnumerable<SimpleUserDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SimpleUserDTO> GetUserByNameAsync(string userLogin)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveUserAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUserAsync(EditUserDTO company)
    {
        throw new NotImplementedException();
    }
}
