
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.DATA.Models;

public class Recipe
{
    public int RecipeId { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    public float Quantity { get; set; }
    public List<RecipeDetail> Details { get; set; }

}
