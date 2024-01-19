
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using System.Data;

namespace SchoolCanteen.DATA.Repositories.CompanyRepo;

public class CompanyRepository : ICompanyRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger<CompanyRepository> logger;

    public CompanyRepository(DatabaseApiContext ctx, ILogger<CompanyRepository> logger)
    {
        this.ctx = ctx;
        this.logger = logger;
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        try
        {
            return await ctx.Companies
                .OrderBy(e => e.Name)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return new List<Company>();
        }
    }
    public async Task<bool> AddAsync(Company company)
    {
        try
        {
            await ctx.AddAsync(company);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return false;
        }
    }
    public async Task<bool> DeleteAsync(Company company)
    {
        try
        {
            ctx.Companies.Remove(company);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return false;
        }

    }
    public async Task<Company> GetByNameAsync(string companyName)
    {
        try
        {
            return await ctx.Companies.FirstOrDefaultAsync(c => c.Name == companyName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return default;
        }

    }
    public async Task<Company> GetByIdAsync(Guid id)
    {
        try
        {
            return await ctx.Companies.FirstOrDefaultAsync(c => c.CompanyId == id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return default;
        }
    }
    public async Task<bool> UpdateAsync(Company company)
    {
        try
        {
            ctx.Companies.Update(company);
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
