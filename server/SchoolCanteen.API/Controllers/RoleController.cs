using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.Logic.Services;

namespace SchoolCanteen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly ILogger<RoleController> logger;
    private readonly IRoleService roleService;

    public RoleController(IRoleService roleService, ILogger<RoleController> logger)
    {
        this.logger = logger;
        this.roleService = roleService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<Role>>> GetAllAsync([FromQuery] Guid companyId)
    {
        try
        {
            return Ok(await roleService.GetAllAsync(companyId));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetByName")]
    public async Task<ActionResult<Role>> GetByNameAsync([FromQuery] string name, Guid companyId)
    {
        try
        {
            var company = await roleService.GetByNameAsync(name, companyId);
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
    public async Task<ActionResult<Role>> CreateAsync([FromBody] string roleName, Guid companyId)
    {
        try
        {
            var role = await roleService.CreateAsync( new Role { RoleName = roleName, CompanyId = companyId });

            return Ok(role);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest($"Could not create {roleName}");
        }
    }

    [HttpPut]
    public async Task<ActionResult<bool>> EditAsync([FromBody] Role editRole)
    {
        try
        {
            return Ok(await roleService.UpdateAsync(editRole));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteAsync([FromQuery] Guid id)
    {
        try
        {
            return Ok(await roleService.DeleteAsync(id));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
}
