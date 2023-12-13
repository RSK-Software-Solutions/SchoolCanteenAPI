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

    public async Task<SimpleCompanyDTO> CreateCompanyAsync(CreateCompanyDTO companyDTO)
    {
        var company = _createCompany.ConvertFromDTO(companyDTO);
        await _companyRepository.AddAsync(company);

        return _simpleCompany.ConvertFromModel(company);
    }
    
    public async Task<IEnumerable<SimpleCompanyDTO>> GetAllAsync()
    {
        var result = new List<SimpleCompanyDTO>();
        var companies = await _companyRepository.GetAllAsync();

        foreach (var company in companies)
        {
            result.Add(_simpleCompany.ConvertFromModel(company));
        }

        return result;
    }

    public async Task<SimpleCompanyDTO> GetCompanyByNameAsync(string companyName)
    {
        var company = await _companyRepository.GetByNameAsync(companyName);
        if (company == null) return null;

        return _simpleCompany.ConvertFromModel(company);
    }

    public async Task<bool> UpdateCompanyAsync(EditCompanyDTO companyDTO)
    {
        var existingCompany = await _companyRepository.GetByIdAsync(companyDTO.CompanyId);
        if (existingCompany == null) return false;

        UpdateProperties<Company>(_editCompany.ConvertFromDTO(companyDTO), existingCompany);

        return await _companyRepository.UpdateAsync(existingCompany);
    }

    public async Task<bool> RemoveCompanyAsync(Guid Id)
    {
        var company = await _companyRepository.GetByIdAsync(Id);
        if (company == null) return false;

        await _companyRepository.DeleteAsync(company);
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