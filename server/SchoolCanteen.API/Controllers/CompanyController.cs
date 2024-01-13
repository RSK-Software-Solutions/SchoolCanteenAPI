using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.Logic.Services.CompanyServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
    [HttpGet, Authorize (Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<SimpleCompanyDTO>>> GetAllAsync()
    {
        try
        {
            var companies = await _companyService.GetAllAsync();
            if (companies.Count() == 0) return NotFound($"No companies found.");

            return Ok(companies);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpPut, Authorize (Roles = "Admin")]
    public async Task<ActionResult<bool>> EditCompanyAsync([FromBody] EditCompanyDTO editCompany)
    {
        try
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
 
            var existingCompany = await _companyService.GetCompanyByIdAsync(editCompany.CompanyId);
            if (existingCompany == null) return NotFound();

            return Ok(await _companyService.UpdateCompanyAsync(editCompany));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete, Authorize(Roles = "Admin")]
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
