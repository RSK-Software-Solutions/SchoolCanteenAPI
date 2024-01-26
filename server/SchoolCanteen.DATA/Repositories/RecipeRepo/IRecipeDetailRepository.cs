
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.DATA.Repositories.RecipeRepo;

public interface IRecipeDetailRepository
{
    Task<IEnumerable<RecipeDetail>> GetAllAsync();
    Task<RecipeDetail> GetByIdAsync(int id);
    Task<bool> AddAsync(RecipeDetail detail);
    Task<bool> DeleteAsync(RecipeDetail detail);
}
