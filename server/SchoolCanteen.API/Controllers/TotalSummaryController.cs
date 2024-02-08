using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.Logic.DTOs.TotalSummaryDTOs;
using SchoolCanteen.Logic.Services.TotalSummaryServices;

namespace SchoolCanteen.API.Controllers;

public class TotalSummaryController : ControllerBase
{
    private readonly ILogger<TotalSummaryController> logger;
    private readonly ITotalSummaryService _summaryService;
    public TotalSummaryController(ITotalSummaryService summaryService, ILogger<TotalSummaryController> logger)
    {
        this.logger = logger;
        _summaryService = summaryService;
    }

    [HttpGet("/api/summary"), Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<BasicTotalSummaryDTO>>> GetSummaryAsync()
    {
        try
        {
            var summary = await _summaryService.GetTotalSummary();

            return Ok(summary);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
}
