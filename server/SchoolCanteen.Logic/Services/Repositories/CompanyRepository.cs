
using Microsoft.EntityFrameworkCore;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Services.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly DatabaseApiContext ctx;

    public CompanyRepository(DatabaseApiContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        try
        {
            return await ctx.Companies.OrderBy(e => e.Name).ToListAsync();
        }
        catch (Exception ex)
        {
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
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Company company)
    {
        ctx.Companies.Remove(company);
        await ctx.SaveChangesAsync();
        return true;
    }

    public async Task<Company> GetByNameAsync(string companyName)
    {
        return await ctx.Companies.FirstOrDefaultAsync(c => c.Name == companyName);
    }

    public async Task<Company> GetByIdAsync(Guid id)
    {
        return await ctx.Companies.FirstOrDefaultAsync(c => c.CompanyId == id);
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
            return false;
        }
    }

}
