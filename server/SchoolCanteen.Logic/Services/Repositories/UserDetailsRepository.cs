
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.Services.Repositories.Interfaces;

namespace SchoolCanteen.Logic.Services.Repositories;

public class UserDetailsRepository : IUserDetailsRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger logger;

    public UserDetailsRepository(DatabaseApiContext ctx, ILogger logger)
    {
        this.ctx = ctx;
        this.logger = logger;
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
            logger.LogError(ex.Message, ex);
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
