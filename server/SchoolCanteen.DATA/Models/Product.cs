
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
    public float MinQuantity { get; set; } = 0;
    public bool Active { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    //public List<ProductFinishedProduct> ProductFinishedProducts { get; set; }
    public List<FinishedProduct> FinishedProducts { get; set; } = new List<FinishedProduct>();
    public List<RecipeDetail> RecipeDetails { get; set; } = new List<RecipeDetail>();
    public List<ProductStorage> ProductStorages { get; set; } = new List<ProductStorage>();
}
