
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Services.Repositories;

public class UserDetailsRepository : IUserDetailsRepository
{
    private readonly DatabaseApiContext ctx;

    public UserDetailsRepository(DatabaseApiContext ctc)
    {
        this.ctx = ctc;
    }

    public async Task<bool> AddAsync(UserDetails userDetails)
    {
        try
        {
            await ctx.UsersDetails.AddAsync(userDetails);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Task<bool> DeleteAsync(UserDetails userDetails)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDetails>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(UserDetails userDetails)
    {
        throw new NotImplementedException();
    }
}
