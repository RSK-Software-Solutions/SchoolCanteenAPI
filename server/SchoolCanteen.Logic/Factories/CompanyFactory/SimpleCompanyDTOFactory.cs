using SchoolCanteen.Logic.DTOs.Company;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Factories.CompanyFactory;

internal class SimpleCompanyDTOFactory : ICompanyDTOFactory<SimpleCompanyDTO>
{
    public SimpleCompanyDTO ConvertFromModel(Company company)
    {
        return new SimpleCompanyDTO
        {
            CompanyId = company.CompanyId,
            Name = company.Name,
        };
    }

    public Company ConvertFromDTO(SimpleCompanyDTO dto)
    {
        return new Company
        {
            CompanyId = dto.CompanyId,
            Name = dto.Name,
        };
    }
}
