
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Services.Repositories;

internal class RoleRepository : IRoleRepository
{
    private readonly DatabaseApiContext ctx;

    public RoleRepository(DatabaseApiContext ctx)
    {
        this.ctx = ctx;
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
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Role role)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Role>> GetAllAsync(Guid companyId)
    {
        throw new NotImplementedException();
    }

    public async Task<Role> GetByNameAsync(string roleName, Guid companyId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Role role)
    {
        throw new NotImplementedException();
    }
}
