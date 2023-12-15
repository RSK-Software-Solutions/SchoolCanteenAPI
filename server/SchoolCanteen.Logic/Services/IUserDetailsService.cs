
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Services;

public interface IUserDetailsService
{
    Task<UserDetails> CreateAsync(UserDetails userDetails);
    Task<bool> UpdateAsync(UserDetails userDetails);
    Task<bool> RemoveAsync(Guid Id);
}
