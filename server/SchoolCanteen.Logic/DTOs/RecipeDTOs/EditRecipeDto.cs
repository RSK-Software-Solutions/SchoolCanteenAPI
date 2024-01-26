
using SchoolCanteen.DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.RecipeDTOs;

public class EditRecipeDto
{
    public int RecipeId { get; set; }
    [Required] public int UnitId { get; set; }
    public Unit Unit { get; set; }
    [MaxLength(100)]
    [Required] public string Name { get; set; }
    public float Quantity { get; set; } = 0;
    public List<RecipeDetail> Details { get; set; }
}
