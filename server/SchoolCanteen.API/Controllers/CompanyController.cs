using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;

using SchoolCanteen.Logic.Services;
using SchoolCanteen.Logic.Services.Interfaces;

namespace SchoolCanteen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ILogger<CompanyController> logger;
    private readonly ICompanyService companyService;

    public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
    {
        this.logger = logger;
        this.companyService = companyService;

    }

    // GET: api/<CompanyController>
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<SimpleCompanyDTO>>> GetAllAsync()
    {
        try
        {
            var companies = await companyService.GetAllAsync();
            if (companies.Count() == 0) return NotFound($"No companies found.");

            return Ok(companies);
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
            var company = await companyService.GetCompanyByNameAsync(name);
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
            var company = await companyService.CreateCompanyAsync(createCompany);

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
            var existingCompany = await companyService.GetCompanyByNameAsync(editCompany.Name);
            if (existingCompany == null) return NotFound();

            return Ok(await companyService.UpdateCompanyAsync(editCompany));
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
            return Ok (await companyService.RemoveCompanyAsync(id));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    } 
}
