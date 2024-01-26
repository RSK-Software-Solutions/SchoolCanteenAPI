using SchoolCanteen.Logic.DTOs.RecipeDTOs;

namespace SchoolCanteen.Logic.Services.RecipeServices;

public interface IRecipeService
{
    Task<IEnumerable<SimpleRecipeDto>> GetAllAsync();
    Task<SimpleRecipeDto> GetByNameAsync(string name);
    Task<SimpleRecipeDto> CreateAsync(CreateRecipeDto dto);
    Task<bool> DeleteAsync(int Id);
    Task<bool> UpdateAsync(EditRecipeDto dto);
    Task<SimpleRecipeDto> AddProductToRecipe(CreateRecipeDetailsDto dto);
}
