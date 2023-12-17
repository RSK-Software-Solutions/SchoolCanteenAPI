
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.Services.Repositories.Interfaces;

namespace SchoolCanteen.Logic.Services.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger logger;

    public UserRepository(DatabaseApiContext ctc, ILogger logger)
    {
        this.ctx = ctc;
        this.logger = logger;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        try
        {
            return await ctx.Users.OrderBy(e => e.Login).ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return new List<User>();
        }
    }

    public async Task<bool> AddAsync(User user)
    {
        try
        {
            await ctx.Users.AddAsync(user);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(User user)
    {
        ctx.Users.Remove(user);
        await ctx.SaveChangesAsync();
        return true;
    }


    public async Task<User> GetByIdAsync(Guid id)
    {
        return await ctx.Users.FirstOrDefaultAsync(e => e.UserId == id);
    }

    public async Task<User> GetByNameAsync(string userLogin)
    {
        return await ctx.Users.FirstOrDefaultAsync(e => e.Login == userLogin);
    }

    public async Task<bool> UpdateAsync(User user)
    {
        try
        {
            ctx.Users.Update(user);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
