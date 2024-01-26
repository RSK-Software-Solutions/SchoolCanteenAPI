
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.RecipeRepo;

public class RecipeRepository : IRecipeRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger<RecipeRepository> logger;
    public RecipeRepository(DatabaseApiContext ctx, ILogger<RecipeRepository> logger)
    {
        this.ctx = ctx;
        this.logger = logger;
    }

    /// <summary>
    /// Asynchronously adds a new Recipe to the database.
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> AddAsync(Recipe recipe)
    {
        try
        {
            await ctx.AddAsync(recipe);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// Asynchronously deletes a Recipe from the database.
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns>
    /// A Task representing the asynchronous operation. The Task result is a boolean value:
    /// - True if the Recipe is successfully deleted from the database.
    /// - Throws an exception with details if there is an issue deleting the Unit.</returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> DeleteAsync(Recipe recipe)
    {
        try
        {
            ctx.Recipes.Remove(recipe);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// Asynchronously retrieves all Recipes objects from the database.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IEnumerable<Recipe>> GetAllAsync(Guid companyId)
    {
        try
        {
            return await ctx.Recipes
                .Where(e => e.CompanyId ==  companyId)
                .Include(e => e.Details)
                    .ThenInclude(d => d.Product)
                        .ThenInclude(p => p.Unit)
                .OrderBy(e => e.Name)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// Asynchronously retrieves a Recipe object from the database based on its identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Recipe> GetByIdAsync(int id, Guid companyId)
    {
        try
        {
            return await ctx.Recipes
                .Include(e => e.Details)
                .FirstOrDefaultAsync(e => e.RecipeId == id &&  e.CompanyId == companyId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// Asynchronously retrieves a Recipe object from the database based on its name.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="companyId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Recipe> GetByNameAsync(string name, Guid companyId)
    {
        try
        {
            return await ctx.Recipes
                .Include(e => e.Details)
                .FirstOrDefaultAsync(e => e.Name == name && e.CompanyId == companyId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(Recipe recipe)
    {
        try
        {
            ctx.Recipes.Update(recipe);
            await ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return false;
        }
    }
}
