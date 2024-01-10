using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using AutoMapper;
using System.Data;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Repositories.CompanyRepo;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.Services.User;

namespace SchoolCanteen.Logic.Services.CompanyServices;

public class CompanyService : ICompanyService
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    private readonly ICompanyRepository _companyRepository;

    public CompanyService(DatabaseApiContext databaseApiContext,
        IMapper mapper,
        ILogger<CompanyService> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _companyRepository = new CompanyRepository(databaseApiContext, logger);
    }

    public async Task<Company> CreateCompanyAsync(string companyName)
    {
        var existCompany = await _companyRepository.GetByNameAsync(companyName);
        if (existCompany != null)
        {
            _logger.LogInformation($"Company {companyName} already exists.");
            return existCompany;
        }

        var company = new Company { Name = companyName };
        await _companyRepository.AddAsync(company);

        return company;
    }

    public async Task<IEnumerable<SimpleCompanyDTO>> GetAllAsync()
    {
        List<DATA.Models.Company> companies = (await _companyRepository.GetAllAsync()).ToList();

        return companies.Select(company => _mapper.Map<SimpleCompanyDTO>(company));
    }

    public async Task<Company> GetCompanyByNameAsync(string companyName)
    {
        var company = await _companyRepository.GetByNameAsync(companyName);
        if (company == null) return null;

        return company;
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
}