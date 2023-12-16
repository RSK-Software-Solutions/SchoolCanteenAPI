
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SchoolCanteen.Logic.Services.Repositories;

internal class RoleRepository : IRoleRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger logger;

    public RoleRepository(DatabaseApiContext ctx, ILogger<RoleRepository> logger)
    {
        this.ctx = ctx;
        this.logger = logger;
    }

    public async Task<bool> AddAsync(Role role)
    {
        try
        {
            await ctx.Roles.AddAsync(role);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Role role)
    {
        try
        {
            ctx.Roles.Remove(role);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return false;
        }

    }

    public async Task<IEnumerable<Role>> GetAllAsync(Guid companyId)
    {
        try
        {
            return await ctx.Roles
                .Where(e => e.CompanyId == companyId)
                .OrderBy(e => e.RoleName)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return Enumerable.Empty<Role>();
        }
    }

    public async Task<Role> GetByNameAsync(string roleName, Guid companyId)
    {
        try
        {
            return ctx.Roles
                .FirstOrDefault(e => e.CompanyId == companyId && e.RoleName == roleName);
        }
        catch (Exception ex)
        {
            logger?.LogError(ex.Message);
            return null;
        }
    }
    public async Task<Role> GetByIdAsync(Guid id)
    {
        try
        {
            return await ctx.Roles
            .FirstOrDefaultAsync(e => e.CompanyId == id && e.RoleId == id);
        }
        catch (Exception ex)
        {
            logger?.LogError(ex.Message);
            return null;
        }
    }

    public async Task<bool> UpdateAsync(Role role)
    {
        try
        {
            ctx.Roles.Update(role);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex.Message);
            return false;
        }
    }
}
