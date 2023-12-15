using SchoolCanteen.Logic.DTOs.UserDTOs;

namespace SchoolCanteen.Logic.Services;

public interface IUserService
{
    Task<SimpleUserDTO> CreateUserAsync(CreateUserDTO userDto);
    Task<bool> UpdateUserAsync(EditUserDTO userDto);
    Task<bool> RemoveUserAsync(Guid Id);
    Task<SimpleUserDTO> GetUserByNameAsync(string userLogin);
    Task<IEnumerable<SimpleUserDTO>> GetAllAsync();
}
