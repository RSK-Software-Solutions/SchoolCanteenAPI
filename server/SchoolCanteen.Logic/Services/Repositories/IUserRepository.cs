using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Services.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByNameAsync(string userName);
    Task<User> GetByIdAsync(Guid id);
    Task<bool> AddAsync(User user);
    Task<bool> DeleteAsync(User user);
    Task<bool> UpdateAsync(User user);
}
