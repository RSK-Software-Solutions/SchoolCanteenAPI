
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.Company;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SchoolCanteen.Logic.Services.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly DatabaseApiContext ctx;

    public CompanyRepository(DatabaseApiContext ctx)
    {
        this.ctx = ctx;
    }
    public IEnumerable<Company> GetAll()
    {
        try
        {
            return ctx.Companies.OrderBy(e => e.Name).ToList();
        }
        catch (Exception ex)
        {
            return new List<Company>();
        }
    }
    public async Task<bool> Add(Company company)
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

    public async Task<bool> Delete(Company company)
    {
        ctx.Companies.Remove(company);
        await ctx.SaveChangesAsync();
        return true;
    }

    public Company GetByName(string companyName)
    {
        return ctx.Companies.FirstOrDefault(c => c.Name == companyName);
    }

    public Company GetById(Guid id)
    {
        return ctx.Companies.FirstOrDefault(c => c.CompanyId == id);
    }

    public async Task<bool> Update(Company company)
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
