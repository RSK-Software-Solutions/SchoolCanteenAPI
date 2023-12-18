using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(Guid companyId);
    Task<User> GetByNameAsync(string userName, Guid companyId);
    Task<User> GetByIdAsync(Guid id);
    Task<bool> AddAsync(User newUser);
    Task<bool> DeleteAsync(User user);
    Task<bool> UpdateAsync(User user);
}
