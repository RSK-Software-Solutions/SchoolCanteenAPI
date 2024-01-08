using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.RoleDTOs;
using SchoolCanteen.Logic.Services.Interfaces;
using SchoolCanteen.Logic.Services.Roles;

namespace SchoolCanteen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly ILogger<RoleController> logger;
    private readonly IRoleService roleService;
    private readonly IRolesService rolesService;

    public RoleController(ILogger<RoleController> logger, IRoleService roleService, IRolesService rolesService )
    {
        this.logger = logger;
        this.roleService = roleService;
        this.rolesService = rolesService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<SimpleRoleDTO>>> GetAllAsync([FromQuery] Guid companyId)
    {
        try
        {
            var roles = await roleService.GetAllAsync(companyId);
            if (roles.Count() == 0) return NotFound($"No roles found.");

            return Ok(roles);
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

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<Role>> CreateAsync([FromBody] string roleName, Guid companyId)
    {
        try
        {
            //var role = await roleService.CreateAsync( new Role { RoleName = roleName, CompanyId = companyId });
            var role = await rolesService.CreateRoleAsync(roleName);

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
