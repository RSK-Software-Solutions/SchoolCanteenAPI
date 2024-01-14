
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.DATA.Models;

public class Product
{
    public int ProductId { get; set; }
    [Required] public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    [MaxLength(100)]
    [Required] public string Name { get; set; }
    public float Price { get; set; } = 0;
    public float Quantity { get; set; } = 0;
    public int ValidityPeriod { get; set; } = 0;
    //public List<ProductFinishedProduct> ProductFinishedProducts { get; set; }
    public List<FinishedProduct> FinishedProducts { get; set; } = new List<FinishedProduct>();
    public List<RecipeDetail> Details { get; set; } = new List<RecipeDetail>();
}
