
namespace SchoolCanteen.DATA.Models;

public class Recipes
{
    public int RecipeId { get; set; }
    public Guid CompanyId { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public string Name { get; set; }
    public float Quantity { get; set; }

}
