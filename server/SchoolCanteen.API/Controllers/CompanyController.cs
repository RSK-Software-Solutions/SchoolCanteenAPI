using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.Logic.DTOs.Company;
using SchoolCanteen.Logic.Services;

namespace SchoolCanteen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    // GET: api/<CompanyController>
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<SimpleCompanyDTO>> GetAll()
    {
        try
        {
            return Ok(_companyService.GetAll());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET api/<CompanyController>/5
    [HttpGet("GetByName")]
    public ActionResult<SimpleCompanyDTO> GetByName([FromQuery] string name)
    {
        try
        {
            var company = _companyService.GetCompanyByName(name);
            if (company == null) return NotFound();

            return Ok(_companyService.GetCompanyByName(name));
        }
        catch (Exception ex)
        {
            return BadRequest($"Could not find {name}");
        }
    }

    [HttpPost]
    public ActionResult<SimpleCompanyDTO> CreateCompany([FromBody] CreateCompanyDTO createCompany)
    {
        try
        {
            var company = _companyService
                .CreateCompany(new CreateCompanyDTO { Name = createCompany.Name });

            return Ok (company); 
        }
        catch (Exception ex)
        {
            return BadRequest($"Could not create {createCompany.Name}");
        }
    }

    [HttpPut]
    public ActionResult<bool> EditCompany([FromBody] EditCompanyDTO editCompany)
    {
        try
        {
            var result = _companyService
                .UpdateCompany(editCompany);
            return Ok(true);
        }
        catch (Exception ex)
        { 
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public ActionResult<bool> DeleteCompany([FromQuery]Guid id)
    {
        try
        {
            return Ok (_companyService.RemoveCompany(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    } 
    
}
