using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Services.Repositories.Interfaces;

public interface IUserDetailsRepository
{
    Task<IEnumerable<UserDetails>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id);
    Task<bool> AddAsync(UserDetails userDetails);
    Task<bool> DeleteAsync(UserDetails userDetails);
    Task<bool> UpdateAsync(UserDetails userDetails);
}
