
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.Interfaces;

namespace SchoolCanteen.DATA.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger<UserRepository> logger;

    public UserRepository(DatabaseApiContext ctc, ILogger<UserRepository> logger)
    {
        this.ctx = ctc;
        this.logger = logger;
    }

    public async Task<IEnumerable<User>> GetAllAsync(Guid companyId)
    {
        try
        {
            return await ctx.Users
                .Include(x => x.Roles)
                .Where(e => e.CompanyId == companyId)
                .OrderBy(e => e.Login)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return new List<User>();
        }
    }

    public async Task<bool> AddAsync(User newUser)
    {
        try
        {
            var user = await ctx.Users.FirstOrDefaultAsync(e => e.Login == newUser.Login && e.CompanyId == newUser.CompanyId);
            if (user != null) 
            { 
                logger.LogError($"User {newUser.Login} already exists in {user.Company.Name}. "); 
                return false;
            }

            await ctx.Users.AddAsync(newUser);
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
        return await ctx.Users
            .Include(d => d.UserDetails)
            .Include(d => d.Company)
            .FirstOrDefaultAsync(e => e.UserId == id);
    }

    public async Task<User> GetByNameAsync(string userLogin, Guid companyId)
    {
        return await ctx.Users
            .Include(d => d.UserDetails)
            .Include(d => d.Company)
            .FirstOrDefaultAsync(e => e.Login == userLogin && e.CompanyId == companyId);
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
