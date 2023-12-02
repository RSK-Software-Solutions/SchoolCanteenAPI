using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.API.Models;
using SchoolCanteen.Logic.DTOs.Company;
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
    public ActionResult<IEnumerable<SimpleCompanyDTO>> Get()
    {
        try
        {
            var companies = _companyService.GetAll();
            return Ok(companies);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    // GET api/<CompanyController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<CompanyController>
    [HttpPost]
    public ActionResult<SimpleCompanyDTO> Post([FromBody] CreateCompanyData createCompany)
    {
        //var createCompany = JsonSerializer.Deserialize<CreateCompanyDTO>(body);

        try
        {
            var company = _companyService.CreateCompany(new CreateCompanyDTO { Name = createCompany.Name });
            return Ok (company);
        }
        catch (Exception ex)
        {
            return BadRequest($"Could not create {createCompany.Name}");
        }
    }

    // PUT api/<CompanyController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<CompanyController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
