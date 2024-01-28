using SchoolCanteen.Logic.DTOs.RecipeDTOs;

namespace SchoolCanteen.Logic.Services.RecipeServices;

public interface IRecipeService
{
    Task<IEnumerable<SimpleRecipeDto>> GetAllAsync();
    Task<SimpleRecipeDto> GetByIdAsync(int id);
    Task<SimpleRecipeDto> CreateAsync(CreateRecipeDto dto);
    Task<bool> DeleteAsync(int Id);
    Task<bool> UpdateAsync(EditRecipeDto dto);
    Task<SimpleRecipeDto> AddProductToRecipe(CreateRecipeDetailsDto dto);
    Task<bool> DeleteProductFromRecipe(DeleteRecipeDetailsDto dto);
}
