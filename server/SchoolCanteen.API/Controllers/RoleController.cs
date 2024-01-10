using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.Logic.DTOs.RoleDTOs;
using SchoolCanteen.Logic.Services.Roles;

namespace SchoolCanteen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly ILogger<RoleController> logger;
    private readonly IRolesService rolesService;

    public RoleController(ILogger<RoleController> logger, IRolesService rolesService )
    {
        this.logger = logger;
        this.rolesService = rolesService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<SimpleRoleDTO>>> GetAllAsync()
    {
        try
        {
            var roles = new List<IdentityRole>();
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
    public async Task<ActionResult<SimpleRoleDTO>> GetByNameAsync([FromQuery] string name)
    {
        try
        {
            

            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest($"Could not find {name}");
        }
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<SimpleRoleDTO>> CreateAsync([FromBody] string roleName, Guid companyId)
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
    public async Task<ActionResult<bool>> EditAsync([FromBody] SimpleRoleDTO editRole)
    {
        try
        {
            return Ok();
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
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
}
