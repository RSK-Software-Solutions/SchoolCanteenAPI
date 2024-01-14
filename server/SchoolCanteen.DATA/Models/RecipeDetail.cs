
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.DATA.Models;

public class RecipeDetail
{
    public int RecipeDetailId { get; set; }
    [Required] public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    [Required] public int ProductId { get; set;}
    public Product Product { get; set; }
    [Required] public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public int Quantity { get; set; } = 0;

}
