using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.Logic.DTOs.RecipeDTOs;
using SchoolCanteen.Logic.DTOs.UnitDTOs;
using SchoolCanteen.Logic.Services.RecipeServices;
using SchoolCanteen.Logic.Services.UnitServices;
using System.Data;
using System.Security.AccessControl;

namespace SchoolCanteen.API.Controllers;

public class UnitController : ControllerBase
{
    private readonly ILogger logger;
    private readonly IUnitService unitService;

    public UnitController(IUnitService unitService, ILogger<UnitController> logger)
    {
        this.unitService = unitService;
        this.logger = logger;
    }

    [HttpGet("/api/unit"), Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<SimpleUnitDto>>> GetAllAsync()
    {
        try
        {
            var units = await unitService.GetAllAsync();
            if (units.Count() == 0) return NotFound($"No Unit found.");

            return Ok(units);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
}
