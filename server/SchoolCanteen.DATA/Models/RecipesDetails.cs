
namespace SchoolCanteen.DATA.Models;

public class RecipesDetails
{
    public int RecipeId { get; set; }
    public int ProductId { get; set;}
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public int Quantity { get; set; }
    public List<Product> Products { get; set; }
}
