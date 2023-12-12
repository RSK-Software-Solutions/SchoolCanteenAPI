using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.API.Models;
using SchoolCanteen.Logic.DTOs.Company;
using SchoolCanteen.Logic.DTOs.User;
using SchoolCanteen.Logic.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

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
    [HttpGet]
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
    [HttpGet("{name}")]
    public ActionResult<SimpleCompanyDTO> GetByName(string name)
    {
        try
        {
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

    // PUT api/<CompanyController>/5
    [HttpPut("{id}")]
    public ActionResult<bool> EditCompany(int id, [FromBody] EditCompanyDTO editCompany)
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

    // DELETE api/<CompanyController>/5
    [HttpDelete]
    public ActionResult<bool> DeleteCompany(SimpleCompanyDTO id)
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
