using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.RecipeDTOs;

public class CreateRecipeDetailsDto
{
    [Required] public int RecipeId { get; set; }
    [Required] public int ProductId { get; set; }
    [Required] public int UnitId { get; set; }
    [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public float Quantity { get; set; } = 0;
}
