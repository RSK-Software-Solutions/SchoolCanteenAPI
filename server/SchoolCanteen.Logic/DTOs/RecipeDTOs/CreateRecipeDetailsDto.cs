
using SchoolCanteen.DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.RecipeDTOs;

public class CreateRecipeDetailsDto
{
    [Required] public int RecipeId { get; set; }
    [Required] public int ProductId { get; set; }
    [Required] public int UnitId { get; set; }
    public int Quantity { get; set; } = 0;
}
