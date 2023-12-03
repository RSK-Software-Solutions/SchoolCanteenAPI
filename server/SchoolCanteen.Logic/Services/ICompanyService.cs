using SchoolCanteen.Logic.DTOs.Company;
using SchoolCanteen.Logic.Models;

namespace SchoolCanteen.Logic.Services
{
    public interface ICompanyService
    {
        SimpleCompanyDTO CreateCompany(CreateCompanyDTO company);
        bool UpdateCompany(EditCompanyDTO company);
        bool RemoveCompany(SimpleCompanyDTO company);
        SimpleCompanyDTO GetCompany(string companyName);
        IEnumerable<SimpleCompanyDTO> GetAll();
    }
}