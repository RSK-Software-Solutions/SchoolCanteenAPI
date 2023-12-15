using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.DATA.Models;
using System.Reflection;
using SchoolCanteen.Logic.Services.Repositories;
using SchoolCanteen.DATA.DatabaseConnector;
using AutoMapper;

namespace SchoolCanteen.Logic.Services;

public class CompanyService : ICompanyService
{
    private readonly IMapper _mapper;
    private ICompanyRepository _companyRepository;

    public CompanyService(DatabaseApiContext databaseApiContext, IMapper mapper )
    {
        _mapper = mapper;
        _companyRepository = new CompanyRepository(databaseApiContext);
    }

    public async Task<SimpleCompanyDTO> CreateCompanyAsync(CreateCompanyDTO companyDto)
    {
        var company = _mapper.Map<Company>(companyDto);
        await _companyRepository.AddAsync(company);

        return _mapper.Map<SimpleCompanyDTO>(company);
    }
    
    public async Task<IEnumerable<SimpleCompanyDTO>> GetAllAsync()
    {
        List<Company> companies = (await _companyRepository.GetAllAsync()).ToList();

        return companies.Select(company => _mapper.Map<SimpleCompanyDTO>(company));
    }

    public async Task<SimpleCompanyDTO> GetCompanyByNameAsync(string companyName)
    {
        var company = await _companyRepository.GetByNameAsync(companyName);
        if (company == null) return null;

        return _mapper.Map<SimpleCompanyDTO>(company);
    }

    public async Task<bool> UpdateCompanyAsync(EditCompanyDTO companyDto)
    {
        var existingCompany = await _companyRepository.GetByIdAsync(companyDto.CompanyId);
        if (existingCompany == null) return false;

        _mapper.Map(companyDto, existingCompany);

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