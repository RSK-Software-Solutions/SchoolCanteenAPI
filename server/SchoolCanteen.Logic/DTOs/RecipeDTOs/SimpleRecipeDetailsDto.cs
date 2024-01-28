using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.DTOs.UnitDTOs;

namespace SchoolCanteen.Logic.DTOs.RecipeDTOs;

public class SimpleRecipeDetailsDto
{
    public int RecipeDetailId { get; set; }
    public int ProductId { get; set; }
    public SimpleProductDto Product { get; set; }
    public int UnitId { get; set; }
    public SimpleUnitDto Unit { get; set; }
    public int Quantity { get; set; } = 0;
}
