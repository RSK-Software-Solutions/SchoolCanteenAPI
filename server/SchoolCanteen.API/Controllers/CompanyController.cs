using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.Logic.Services.CompanyServices;

namespace SchoolCanteen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[ProducesResponseType(typeof(SimpleCompanyDTO),StatusCodes.Status200OK)]
[ProducesResponseType(typeof(SimpleCompanyDTO), StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CompanyController : ControllerBase
{
    private readonly ILogger<CompanyController> logger;
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService, IMapper mapper, ILogger<CompanyController> logger)
    {
        this.logger = logger;
        _companyService = companyService;

    }

    /// <summary>
    /// Retrieves information about all companies.
    /// </summary>
    /// <returns>
    /// ActionResult<IEnumerable<SimpleCompanyDTO>>:
    /// - Ok with the list of companies as SimpleCompanyDTO if there are companies available.
    /// - NotFound with a message if no companies are found.
    /// - BadRequest with an error message if there is an issue retrieving the companies.
    /// </returns>
    [HttpGet("/api/company"), Authorize (Roles = "Admin")]
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

    /// <summary>
    /// Retrieves company information based on the provided identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the company to retrieve.</param>
    /// <returns>
    /// ActionResult<IEnumerable<EditCompanyDTO>>:
    /// - Ok with the company information as EditCompanyDTO if the company is found.
    /// - BadRequest with an error message if there is an issue retrieving the company.
    /// </returns>
    [HttpGet("/api/company:id"), Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<EditCompanyDTO>>> GetByIdAsync(Guid Id)
    {
        try
        {
            var company = await _companyService.GetCompanyByIdAsync();
            logger.LogInformation("Information");

            return Ok(company);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Edits company information based on the supplied EditCompanyDTO object.
    /// </summary>
    /// <param name="editCompany">Object containing information to update the company.</param>
    /// <returns>
    /// ActionResult<bool>:
    /// - Ok with true if the company has been updated successfully.
    /// - NotFound if the company with the given ID does not exist.
    /// - BadRequest with an error message if there was a problem updating the company.
    /// </returns>>
    [HttpPut("/api/company"), Authorize (Roles = "Admin")]
    public async Task<ActionResult<bool>> EditCompanyAsync([FromBody] EditCompanyDTO editCompany)
    {
        try
        {
            var existingCompany = await _companyService.GetCompanyByIdAsync();
            if (existingCompany == null) return NotFound();

            return Ok(await _companyService.UpdateCompanyAsync(editCompany));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes the company with the given ID.
    /// </summary>
    /// <param name="id">The company ID to delete.</param>
    /// <returns>
    /// /// ActionResult<bool>:
    /// - Ok with true if the company was successfully deleted.
    /// - BadRequest with an error message if there was a problem deleting the company.
    /// </returns>
    [HttpDelete("/api/company"), Authorize(Roles = "Admin")]
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
