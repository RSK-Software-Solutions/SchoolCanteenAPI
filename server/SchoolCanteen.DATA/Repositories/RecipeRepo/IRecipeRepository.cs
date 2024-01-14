
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.RecipeRepo;

public interface IRecipeRepository
{
    Task<IEnumerable<Recipe>> GetAllAsync();
    Task<Recipe> GetByIdAsync(int id);
    Task<bool> AddAsync(Recipe recipe);
    Task<bool> DeleteAsync(Recipe recipe);
}
