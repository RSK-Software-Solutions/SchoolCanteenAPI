using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;

namespace SchoolCanteen.Logic.Services.CompanyServices;

public interface ICompanyService
{
    Task<Company> CreateCompanyAsync(string companyName);
    Task<bool> UpdateCompanyAsync(EditCompanyDTO company);
    Task<bool> RemoveCompanyAsync(Guid Id);
    Task<Company> GetCompanyByNameAsync(string companyName);
    Task<IEnumerable<SimpleCompanyDTO>> GetAllAsync();
}