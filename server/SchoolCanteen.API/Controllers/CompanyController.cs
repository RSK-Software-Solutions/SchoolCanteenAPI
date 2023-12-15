using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.AutoMapperProfiles;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services;

namespace SchoolCanteen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IUserDetailsService _userDetailsService;
    private readonly IMapper _mapper;

    public CompanyController(ICompanyService companyService, IUserService userService, IRoleService roleService, 
        IUserDetailsService userDetailsService, IMapper mapper)
    {
        _companyService = companyService;
        _userService = userService;
        _roleService = roleService;
        _userDetailsService = userDetailsService;
        _mapper = mapper;
    }

    // GET: api/<CompanyController>
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<SimpleCompanyDTO>>> GetAllAsync()
    {
        try
        {
            return Ok(await _companyService.GetAllAsync());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/<CompanyController>/5
    [HttpGet("GetByName")]
    public async Task<ActionResult<SimpleCompanyDTO>> GetByNameAsync([FromQuery] string name)
    {
        try
        {
            var company = await _companyService.GetCompanyByNameAsync(name);
            if (company == null) return NotFound();

            return Ok(company);
        }
        catch (Exception ex)
        {
            return BadRequest($"Could not find {name}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<SimpleCompanyDTO>> CreateCompanyAsync([FromBody] CreateCompanyDTO createCompany)
    {
        try
        {
            var company = await _companyService.CreateCompanyAsync(new CreateCompanyDTO { Name = createCompany.Name });
            var role = await _roleService.CreateAsync(new Role { RoleName = "admin", CompanyId = company.CompanyId });
            var userDetails = await _userDetailsService.CreateAsync(new UserDetails { UserId = Guid.NewGuid() });
            await _userService.CreateUserAsync(new CreateUserDTO { 
                UserId = userDetails.UserId,
                CompanyId = company.CompanyId, 
                RoleId = role.RoleId,
                UserDetailsId = userDetails.UserDetailsId,
                Login = createCompany.Login, 
                Password = createCompany.Password 
            });
            return Ok (company); 
        }
        catch (Exception ex)
        {
            return BadRequest($"Could not create {createCompany.Name}");
        }
    }

    [HttpPut]
    public async Task<ActionResult<bool>> EditCompanyAsync([FromBody] EditCompanyDTO editCompany)
    {
        try
        {
            return Ok(await _companyService.UpdateCompanyAsync(editCompany));
        }
        catch (Exception ex)
        { 
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteCompanyAsync([FromQuery]Guid id)
    {
        try
        {
            return Ok (await _companyService.RemoveCompanyAsync(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    } 
    
}
