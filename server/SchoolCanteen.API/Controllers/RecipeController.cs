using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.DTOs.RecipeDTOs;
using SchoolCanteen.Logic.Services.RecipeServices;
using System.Data;

namespace SchoolCanteen.API.Controllers;

public class RecipeController : ControllerBase
{
    private readonly ILogger logger;
    private readonly IRecipeService recipeService;

    public RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger)
    {
        this.recipeService = recipeService;
        this.logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("/api/recipes"), Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<SimpleRecipeDto>>> GetAllAsync()
    {
        try
        {
            var recipes = await recipeService.GetAllAsync();
            if (recipes.Count() == 0) return NotFound($"No Recipe found.");

            return Ok(recipes);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("/api/recipe"), Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<SimpleRecipeDto>>> GetByIdAsync([FromQuery] int id)
    {
        try
        {
            var recipe = await recipeService.GetByIdAsync(id);
            if (recipe == null) return NotFound($"No Recipe found.");

            return Ok(recipe);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="recipeDto"></param>
    /// <returns></returns>
    [HttpPost("/api/recipe"), Authorize(Roles = "User")]
    public async Task<ActionResult<SimpleRecipeDto>> CreateNewAsync([FromBody] CreateRecipeDto recipeDto)
    {
        try
        {
            var isCreated = await recipeService.CreateAsync(recipeDto);
            if (isCreated == null) return NotFound("Recipe not added. You have no rights.");
            return Ok(isCreated);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="detailsDto"></param>
    /// <returns></returns>
    [HttpPost("/api/recipe/product"), Authorize(Roles = "User")]
    public async Task<ActionResult<SimpleRecipeDto>> AddProductAsync([FromBody] CreateRecipeDetailsDto detailsDto)
    {
        try
        {
            var isCreated = await recipeService.AddProductToRecipe(detailsDto);
            if (isCreated == null) return NotFound("RecipeDetails not added. You have no rights.");
            return Ok(isCreated);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="recipeDto"></param>
    /// <returns></returns>
    [HttpPut("/api/recipe"), Authorize(Roles = "User")]
    public async Task<ActionResult<bool>> UpdateAsync([FromBody] EditRecipeDto recipeDto)
    {
        try
        {
            var isChanged = await recipeService.UpdateAsync(recipeDto);
            if (isChanged == false) return NotFound("Product not found.");

            return Ok(isChanged);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("/api/recipe"), Authorize(Roles = "User")]
    public async Task<ActionResult<bool>> DeleteAsync([FromQuery] int id)
    {
        try
        {
            var isDeleted = await recipeService.DeleteAsync(id);
            if (!isDeleted) return NotFound("Product not found.");

            return Ok(isDeleted);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("/api/recipe/product"), Authorize(Roles = "User")]
    public async Task<ActionResult<SimpleRecipeDto>> DeleteProductAsync([FromQuery] DeleteRecipeDetailsDto recipeDto)
    {
        try
        {
            var isDeleted = await recipeService.DeleteProductFromRecipe(recipeDto);
            if (!isDeleted) return NotFound("RecipeDetails not removed from Recipe. Something goes wrong. Check Ids to remove.");
            return Ok(isDeleted);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
}
