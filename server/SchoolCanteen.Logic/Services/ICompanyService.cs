using SchoolCanteen.Logic.DTOs.Company;

namespace SchoolCanteen.Logic.Services
{
    public interface ICompanyService
    {
        SimpleCompanyDTO CreateCompany(CreateCompanyDTO company);
        bool UpdateCompany(EditCompanyDTO company);


    }
}