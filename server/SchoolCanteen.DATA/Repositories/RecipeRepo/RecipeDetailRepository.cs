﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SchoolCanteen.DATA.Repositories.RecipeRepo;

public class RecipeDetailRepository : IRecipeDetailRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger<RecipeDetailRepository> logger;

    public RecipeDetailRepository(DatabaseApiContext ctx, ILogger<RecipeDetailRepository> logger)
    {
        this.ctx = ctx;
        this.logger = logger;
    }
    /// <summary>
    /// Asynchronously adds a new RecipeDetail to the database.
    /// </summary>
    /// <param name="detail"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> AddAsync(RecipeDetail detail)
    {
        try
        {
            await ctx.AddAsync(detail);
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
    /// Asynchronously deletes a RecipeDetail from the database.
    /// </summary>
    /// <param name="detail"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> DeleteAsync(RecipeDetail detail)
    {
        try
        {
            ctx.RecipeDetails.Remove(detail);
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
    /// Asynchronously retrieves all RecipeDetails objects from the database.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IEnumerable<RecipeDetail>> GetAllAsync()
    {
        try
        {
            return await ctx.RecipeDetails
                .OrderBy(e => e.ProductId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
    /// <summary>
    /// Asynchronously retrieves a RecipeDetail object from the database based on its identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<RecipeDetail> GetByIdAsync(int id)
    {
        try
        {
            return await ctx.RecipeDetails.FirstOrDefaultAsync(e => e.RecipeDetailId == id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
    /// <summary>
    /// Asynchronously updates a RecipeDetail in the database.
    /// </summary>
    /// <param name="detail">The RecipeDetail object to be updated.</param>
    /// <returns>
    /// A Task representing the asynchronous operation. The Task result is a boolean value:
    /// - True if the RecipeDetail is successfully updated in the database.
    /// - False if there is an issue updating the RecipeDetail, and the exception is logged.
    /// </returns>
    public async Task<bool> UpdateAsync(RecipeDetail detail)
    {
        try
        {
            ctx.RecipeDetails.Update(detail);
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
