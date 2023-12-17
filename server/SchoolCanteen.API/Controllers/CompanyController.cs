using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.Logic.Services;

namespace SchoolCanteen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ILogger<CompanyController> logger;
    private readonly ICompanyService _companyService;

    private readonly IMapper _mapper;

    public CompanyController(ICompanyService companyService, IMapper mapper, ILogger<CompanyController> logger)
    {
        this.logger = logger;
        _companyService = companyService;

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
            logger.LogError(ex.Message, ex);
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
            logger.LogError(ex.Message, ex);
            return BadRequest($"Could not find {name}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<SimpleCompanyDTO>> CreateCompanyAsync([FromBody] CreateCompanyDTO createCompany)
    {
        try
        {
            var company = await _companyService.CreateCompanyAsync(createCompany);

            return Ok (company); 
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
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
            logger.LogError(ex.Message, ex);
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
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    } 
}
