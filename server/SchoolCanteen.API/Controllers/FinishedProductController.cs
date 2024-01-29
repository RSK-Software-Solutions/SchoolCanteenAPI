using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.Services.FinishedProductServices;

namespace SchoolCanteen.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class FinishedProductController : ControllerBase
{
    private readonly ILogger<FinishedProductController> logger;
    private readonly IFinishedProductService _finishedProductService;
    public FinishedProductController(IFinishedProductService finishedProductService, ILogger<FinishedProductController> logger)
    {
        this.logger = logger;
        _finishedProductService = finishedProductService;
    }

    /// <summary>
    /// Retrieves all Finished Products asynchronously for the authenticated user.
    /// </summary>
    /// <returns>
    /// Returns an ActionResult containing a collection of SimpleFinishedProductDto.
    /// If there are no Finished Products found, returns a NotFound result.
    /// If an error occurs during the operation, returns a BadRequest result with the error message.
    /// </returns>
    [HttpGet("/api/articles"), Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<SimpleFinishedProductDto>>> GetAllAsync()
    {
        try
        {
            var products = await _finishedProductService.GetAllAsync();
            if (products.Count() == 0) return NotFound($"No Finished Product found.");

            return Ok(products);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("/api/article"), Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<SimpleFinishedProductDto>>> GetByIdAsync(int id)
    {
        try
        {
            var product = await _finishedProductService.GetByIdAsync(id);
            if (product == null) return NotFound($"No Finished Product found.");

            return Ok(product);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Creates a new Finished Product asynchronously for the authenticated user.
    /// </summary>
    /// <param name="createFinishedProductDto">The data to create the new Finished Product.</param>
    /// <returns></returns>
    [HttpPost("/api/article"), Authorize (Roles = "User")]
    public async Task<ActionResult<SimpleFinishedProductDto>> CreateNewAsync([FromBody] CreateFinishedProductDto createFinishedProductDto)
    {
        try
        {
            var newFinishedProduct = await _finishedProductService.CreateAsync(createFinishedProductDto);
            if (newFinishedProduct == null) return BadRequest("Not enough products to realise this recipe.");

            return Ok(newFinishedProduct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Updates a Finished Product asynchronously for the authenticated user based on the specified identifier.
    /// </summary>
    /// <param name="finishedProductDto">The identifier of the Finished Product to update.</param>
    /// <returns></returns>
    [HttpPut("/api/article"), Authorize(Roles = "User")]
    public async Task<ActionResult<bool>> UpdateNewAsync([FromBody] SimpleFinishedProductDto finishedProductDto)
    {
        try
        {
            var newFinishedProduct = await _finishedProductService.UpdateAsync(finishedProductDto);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a Finished Product asynchronously for the authenticated user based on the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the Finished Product to delete.</param>
    /// <returns>
    /// Returns an ActionResult containing a boolean value indicating the success of the deletion.
    /// If the deletion is successful, returns an Ok result.
    /// If the specified Finished Product is not found, returns a NotFound result.
    /// If an error occurs during the deletion, returns a BadRequest result with the error message.
    /// </returns>
    [HttpDelete("/api/article"), Authorize(Roles = "User")]
    public async Task<ActionResult<bool>> DeleteAsync([FromQuery] int id)
    {
        try
        {
            var isDeleted = await _finishedProductService.DeleteAsync(id);
            return Ok(isDeleted);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
}
