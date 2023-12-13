
using Microsoft.Extensions.Configuration;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.Company;

namespace SchoolCanteen.Logic.Services.Repositories;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<Company> GetByNameAsync(string companyName);
    Task<Company> GetByIdAsync(Guid id);
    Task<bool> AddAsync(Company company);
    Task<bool> DeleteAsync(Company company);
    Task<bool> UpdateAsync(Company company);
}
