using SchoolCanteen.Logic.DTOs.UserDTOs;

namespace SchoolCanteen.Logic.Services.Interfaces;

public interface IUserService
{
    Task<SimpleUserDTO> CreateAsync(CreateUserDTO userDto);
    Task<bool> UpdateAsync(EditUserDTO userDto);
    Task<bool> DeleteAsync(Guid Id);
    Task<SimpleUserDTO> GetByNameAsync(string userLogin, Guid companyId);
    Task<IEnumerable<SimpleUserDTO>> GetAllAsync(Guid companyId);
}
