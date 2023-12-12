using SchoolCanteen.Logic.DTOs.Company;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Services
{
    public interface ICompanyService
    {
        SimpleCompanyDTO CreateCompany(CreateCompanyDTO company);
        Task<bool> UpdateCompany(EditCompanyDTO company);
        bool RemoveCompany(SimpleCompanyDTO company);
        SimpleCompanyDTO GetCompanyByName(string companyName);
        IEnumerable<SimpleCompanyDTO> GetAll();
    }
}