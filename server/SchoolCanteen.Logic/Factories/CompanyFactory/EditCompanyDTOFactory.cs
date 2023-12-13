
using SchoolCanteen.Logic.DTOs.Company;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Factories.CompanyFactory;

public class EditCompanyDTOFactory : ICompanyDTOFactory<EditCompanyDTO>
{
    public EditCompanyDTO ConvertFromModel(Company company)
    {
        return new EditCompanyDTO
        {
            CompanyId = company.CompanyId,
            Name = company.Name,
            Nip = company.Nip,
            Street = company.Street,
            Number = company.Number,
            City = company.City,
            PostalCode = company.PostalCode,
            Phone = company.Phone,
            Email = company.Email
        };
    }

    public Company ConvertFromDTO(EditCompanyDTO dto)
    {
        return new Company
        {
            CompanyId = dto.CompanyId,
            Name = dto.Name,
            Nip = dto.Nip,
            Street = dto.Street,
            Number = dto.Number,
            City = dto.City,
            PostalCode = dto.PostalCode,
            Phone = dto.Phone,
            Email = dto.Email
        };
    }
}
