using SchoolCanteen.Logic.DTOs.Company;
using SchoolCanteen.Logic.Factories.CompanyFactory;
using SchoolCanteen.DATA.Models;
using System.Reflection;
using SchoolCanteen.Logic.Services.Repositories;
using SchoolCanteen.DATA.DatabaseConnector;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolCanteen.Logic.Services;

public class CompanyService : ICompanyService
{
    private ICompanyDTOFactory<CreateCompanyDTO> _createCompany;
    private ICompanyDTOFactory<SimpleCompanyDTO> _simpleCompany;
    private ICompanyDTOFactory<EditCompanyDTO> _editCompany;
    private ICompanyRepository _companyRepository;

    public CompanyService(DatabaseApiContext databaseApiContext )
    {
        _createCompany = new CreateCompanyDTOFactory();
        _simpleCompany = new SimpleCompanyDTOFactory();
        _editCompany = new EditCompanyDTOFactory();

        _companyRepository = new CompanyRepository(databaseApiContext);
    }

    public SimpleCompanyDTO CreateCompany(CreateCompanyDTO companyDTO)
    {
        var company = _createCompany.ConvertFromDTO(companyDTO);
        _companyRepository.Add(company);

        return _simpleCompany.ConvertFromModel(company);
    }
    
    public IEnumerable<SimpleCompanyDTO> GetAll()
    {
        var result = new List<SimpleCompanyDTO>();
        foreach (var company in _companyRepository.GetAll())
        {
            result.Add(_simpleCompany.ConvertFromModel(company));
        }

        return result;
    }

    public SimpleCompanyDTO GetCompanyByName(string companyName)
    {
        var company = _companyRepository.GetByName(companyName);
        if (company == null) return null;

        return _simpleCompany.ConvertFromModel(company);
    }

    public async Task<bool> UpdateCompany(EditCompanyDTO companyDTO)
    {
        var existingCompany = _companyRepository.GetById(companyDTO.CompanyId);
        if (existingCompany == null) return false;

        UpdateProperties<Company>(_editCompany.ConvertFromDTO(companyDTO), existingCompany);

        #region simpleVersion
        //existingCompany.Name = companyDTO.Name;
        //existingCompany.Nip = companyDTO.Nip;
        //existingCompany.Street = companyDTO.Street;
        //existingCompany.Number = companyDTO.Number;
        //existingCompany.City = companyDTO.City;
        //existingCompany.PostalCode = companyDTO.PostalCode;
        //existingCompany.Phone = companyDTO.Phone;
        //existingCompany.Email = companyDTO.Email;
        #endregion

        return await _companyRepository.Update(existingCompany);
    }

    public bool RemoveCompany(SimpleCompanyDTO companyDTO)
    {
        var company = _companyRepository.GetById(companyDTO.CompanyId);
        if (company == null) return false;

        _companyRepository.Delete(company);
        return true;
    }

    private void UpdateProperties<T>(T source, T destination)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (property.CanWrite)
            {
                property.SetValue(destination, property.GetValue(source), null);
            }
        }
    }
}