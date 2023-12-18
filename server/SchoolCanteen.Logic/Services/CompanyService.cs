using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.Interfaces;
using SchoolCanteen.DATA.Repositories;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services.Interfaces;
using AutoMapper;
using System.Data;
using Microsoft.Extensions.Logging;

namespace SchoolCanteen.Logic.Services;

public class CompanyService : ICompanyService
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(DatabaseApiContext databaseApiContext, 
        IUserService userService, 
        IRoleService roleService,
        ICompanyRepository companyRepository,
        IMapper mapper, 
        ILogger<CompanyService> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _companyRepository = companyRepository;
        _userService = userService;
        _roleService = roleService;
    }

    public async Task<SimpleCompanyDTO> CreateCompanyAsync(CreateCompanyDTO companyDto)
    {
        var existCompany = await _companyRepository.GetByNameAsync(companyDto.Name);
        if (existCompany != null) 
        {
            _logger.LogInformation($"Company {companyDto.Name} already exists.");
            return _mapper.Map<SimpleCompanyDTO>(existCompany);
        }

        var company = _mapper.Map<Company>(companyDto);
        await _companyRepository.AddAsync(company);

        var role = await CreateAdminRoleForComapany(company);
        
        var userAdmin = await CreateAdminUserForCompany(companyDto, company, role);

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

    private async Task<Role> CreateAdminRoleForComapany(Company company)
    {
        var role = await _roleService.CreateAsync(new Role { RoleName = "admin", CompanyId = company.CompanyId });
        return role;
    }

    private async Task<bool> CreateAdminUserForCompany(CreateCompanyDTO companyDto, Company company, Role role)
    {
        try
        {
            await _userService.CreateAsync(new CreateUserDTO
            {
                CompanyId = company.CompanyId,
                Role = role,
                Login = companyDto.Login,
                Password = companyDto.Password
            });

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message); return false;
        }
    }
}