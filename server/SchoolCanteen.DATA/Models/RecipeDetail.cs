
namespace SchoolCanteen.DATA.Models;

public class RecipeDetail
{
    public int RecipeDetailId { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public int ProductId { get; set;}
    public Product Product { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public int Quantity { get; set; }

}
