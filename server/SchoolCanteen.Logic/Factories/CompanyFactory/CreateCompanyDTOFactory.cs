using SchoolCanteen.Logic.DTOs.Company;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Factories.CompanyFactory;

public class CreateCompanyDTOFactory : ICompanyDTOFactory<CreateCompanyDTO>
{
    public CreateCompanyDTO ConvertFromModel(Company company)
    {
        return new CreateCompanyDTO
        {
            Name = company.Name,
        };
    }

    public Company ConvertFromDTO(CreateCompanyDTO dto)
    {
        return new Company
        {
            CompanyId = Guid.NewGuid(),
            Name = dto.Name,
        };
    }
}
