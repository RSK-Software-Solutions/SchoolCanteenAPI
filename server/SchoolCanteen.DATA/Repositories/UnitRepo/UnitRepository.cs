
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.UnitRepo;

public class UnitRepository : IUnitRepository
{
    private readonly DatabaseApiContext ctx;
    private readonly ILogger<UnitRepository> logger;

    public UnitRepository(DatabaseApiContext ctx, ILogger<UnitRepository> logger)
    {
        this.logger = logger;
        this.ctx = ctx;
    }
    /// <summary>
    /// Asynchronously adds a new Unit to the database.
    /// </summary>
    /// <param name="unit">The Unit object to be added.</param>
    /// <returns>
    /// A Task representing the asynchronous operation. The Task result is a boolean value:
    /// - True if the Unit is successfully added to the database.
    /// - Throws an exception with details if there is an issue adding the Unit.
    /// </returns>
    public async Task<bool> AddAsync(Unit unit)
    {
        try
        {
            await ctx.AddAsync(unit);
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
    /// Asynchronously deletes a Unit from the database.
    /// </summary>
    /// <param name="unit">The Unit object to be deleted.</param>
    /// <returns>
    /// A Task representing the asynchronous operation. The Task result is a boolean value:
    /// - True if the Unit is successfully deleted from the database.
    /// - Throws an exception with details if there is an issue deleting the Unit.
    /// </returns>
    public async Task<bool> DeleteAsync(Unit unit)
    {
        try
        {
            ctx.Units.Remove(unit);
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
    /// Asynchronously retrieves all Unit objects from the database.
    /// </summary>
    /// <returns>
    /// A Task representing the asynchronous operation. The Task result is an IEnumerable<Unit>:
    /// - An IEnumerable<Unit> containing all Unit objects, ordered by their names.
    /// - Throws an exception with details if there is an issue retrieving the Units.
    /// </returns>
    public async Task<IEnumerable<Unit>> GetAllAsync(Guid companyId)
    {
        try
        {
            return await ctx.Units
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
    /// Asynchronously retrieves a Unit object from the database based on its identifier.
    /// </summary>
    /// <param name="id">The identifier of the Unit to retrieve.</param>
    /// <returns>
    /// A Task representing the asynchronous operation. The Task result is a Unit object:
    /// - The Unit object with the specified identifier if found.
    /// - Null if no Unit is found with the specified identifier.
    /// - Throws an exception with details if there is an issue retrieving the Unit.
    /// </returns>
    public async Task<Unit> GetByIdAsync(int id)
    {
        try
        {
            return await ctx.Units.FirstOrDefaultAsync(e => e.UnitId == id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<Unit> GetByNameAsync(string name, Guid companyId)
    {
        try
        {
            return await ctx.Units.FirstOrDefaultAsync(e => e.Name == name && e.CompanyId == companyId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
}
