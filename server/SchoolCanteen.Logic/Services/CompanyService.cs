using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.Services.Repositories.Interfaces;
using SchoolCanteen.DATA.DatabaseConnector;
using AutoMapper;
using SchoolCanteen.Logic.Services.Repositories;
using Microsoft.Extensions.Logging;
using System.Data;
using SchoolCanteen.Logic.DTOs.UserDTOs;

namespace SchoolCanteen.Logic.Services;

public class CompanyService : ICompanyService
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IUserDetailsService _userDetailsService;
    private readonly ILogger logger;

    public CompanyService(DatabaseApiContext databaseApiContext, 
        IUserService userService, 
        IRoleService roleService,
        IUserDetailsService userDetailsService,
        IMapper mapper, 
        ILogger<CompanyService> logger)
    {
        _mapper = mapper;
        this.logger = logger;
        _companyRepository = new CompanyRepository(databaseApiContext, logger);
        _userService = userService;
        _roleService = roleService;
        _userDetailsService = userDetailsService;
    }

    public async Task<SimpleCompanyDTO> CreateCompanyAsync(CreateCompanyDTO companyDto)
    {
        var existCompany = await _companyRepository.GetByNameAsync(companyDto.Name);
        if (existCompany != null) 
        {
            logger.LogInformation($"Company {companyDto.Name} already exists.");
            return _mapper.Map<SimpleCompanyDTO>(existCompany);
        }

        var company = _mapper.Map<Company>(companyDto);
        await _companyRepository.AddAsync(company);

        var role = await CreateAdminRoleForComapany(company);
        var userDetails = await CreateEmptyUserDetailsRecord();
        var userAdmin = await CreateAdminUserForCompany(companyDto, company, role, userDetails);

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

    private async Task<UserDetails> CreateEmptyUserDetailsRecord()
    {
        var userDetails = await _userDetailsService.CreateAsync(new UserDetails { UserId = Guid.NewGuid() });
        return userDetails;
    }

    private async Task<bool> CreateAdminUserForCompany(CreateCompanyDTO companyDto, Company company, Role role, UserDetails userDetails)
    {
        try
        {
            await _userService.CreateUserAsync(new CreateUserDTO
            {
                UserId = userDetails.UserId,
                CompanyId = company.CompanyId,
                RoleId = role.RoleId,
                UserDetailsId = userDetails.UserDetailsId,
                Login = companyDto.Login,
                Password = companyDto.Password
            });

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message); return false;
        }
    }
}