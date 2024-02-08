
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.RecipeRepo;

public interface IRecipeRepository
{
    Task<IEnumerable<Recipe>> GetAllAsync(Guid companyId);
    Task<Recipe> GetByIdAsync(int id, Guid companyId);
    Task<Recipe> GetByNameAsync(string name, Guid companyId);
    Task<bool> AddAsync(Recipe recipe);
    Task<bool> DeleteAsync(Recipe recipe);
    Task<bool> UpdateAsync(Recipe recipe);
    Task<int> CountAsync(Guid companyId);
}
