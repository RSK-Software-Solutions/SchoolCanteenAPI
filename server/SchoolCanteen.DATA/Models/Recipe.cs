
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.DATA.Models;

public class Recipe
{
    public int RecipeId { get; set; }
    [Required] public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    [Required] public int UnitId { get; set; }
    public Unit Unit { get; set; }
    [MaxLength(100)]
    [Required] public string Name { get; set; }
    public float Quantity { get; set; } = 0;
    public int ValidityPeriod { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<RecipeDetail> Details { get; set; } = new List<RecipeDetail>();

}
