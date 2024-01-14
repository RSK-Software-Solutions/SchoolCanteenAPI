using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using AutoMapper;
using System.Data;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Repositories.CompanyRepo;
using SchoolCanteen.DATA.Models;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace SchoolCanteen.Logic.Services.CompanyServices;

public class CompanyService : ICompanyService
{
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    private readonly ICompanyRepository _companyRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor  _context;

    public CompanyService(DatabaseApiContext databaseApiContext,
        IMapper mapper,
        ILogger<CompanyService> logger,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor context)
    {
        _mapper = mapper;
        _logger = logger;
        _companyRepository = new CompanyRepository(databaseApiContext, logger);
        _userManager = userManager;
        _context = context;
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

    public async Task<EditCompanyDTO> GetCompanyByIdAsync(Guid companyId)
    {
        var company = await _companyRepository.GetByIdAsync(companyId);
        if (company == null) return null;

        return _mapper.Map<EditCompanyDTO>(company);
    }

    public async Task<bool> UpdateCompanyAsync(EditCompanyDTO companyDto)
    {
        if (!await IsUserMatchedWithCompany(companyDto.CompanyId)) return false;

        var existingCompany = await _companyRepository.GetByIdAsync(companyDto.CompanyId);
        if (existingCompany == null) return false;

        _mapper.Map(companyDto, existingCompany);

        return await _companyRepository.UpdateAsync(existingCompany);
    }

    public async Task<bool> RemoveCompanyAsync(Guid Id)
    {
        if (await IsUserMatchedWithCompany(Id)) return false;

        var company = await _companyRepository.GetByIdAsync(Id);
        if (company == null) return false;

        await _companyRepository.DeleteAsync(company);
        return true;
    }

    private async Task<bool> IsUserMatchedWithCompany(Guid CompanyId)
    {
        var httpContext = _context.HttpContext ?? throw new InvalidOperationException("HttpContext not available");

        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId);
        if (user.CompanyId != CompanyId) return false;
        return true;
    }
}