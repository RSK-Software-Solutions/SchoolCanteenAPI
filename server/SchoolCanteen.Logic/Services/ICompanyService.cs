using SchoolCanteen.Logic.DTOs.CompanyDTOs;

namespace SchoolCanteen.Logic.Services;

public interface ICompanyService
{
    Task<SimpleCompanyDTO> CreateCompanyAsync(CreateCompanyDTO company);
    Task<bool> UpdateCompanyAsync(EditCompanyDTO company);
    Task<bool> RemoveCompanyAsync(Guid Id);
    Task<SimpleCompanyDTO> GetCompanyByNameAsync(string companyName);
    Task<IEnumerable<SimpleCompanyDTO>> GetAllAsync();
}