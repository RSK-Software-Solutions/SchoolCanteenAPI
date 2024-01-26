
using SchoolCanteen.Logic.DTOs.RecipeDTOs;

namespace SchoolCanteen.Logic.Services.RecipeServices;

public interface IRecipeDetailsService
{
    Task<IEnumerable<SimpleRecipeDetailsDto>> GetAllAsync();
    Task<SimpleRecipeDetailsDto> CreateAsync(CreateRecipeDetailsDto dto);
    Task<bool> DeleteAsync(int Id);
}
