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

    [HttpGet("/api/roles")]
    public async Task<ActionResult<IEnumerable<SimpleRoleDTO>>> GetAllAsync()
    {
        try
        {
            var roles = await rolesService.GetRolesAsync();
            if (roles.Count() == 0) return NotFound($"No roles found.");

            return Ok(roles);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

}
