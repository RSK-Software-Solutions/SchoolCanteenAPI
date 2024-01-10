
namespace SchoolCanteen.DATA.Models;

public class Product
{
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public float Quantity { get; set; }
    public int ValidityPeriod { get; set; }
    //public List<ProductFinishedProduct> ProductFinishedProducts { get; set; }
    public List<FinishedProduct> FinishedProducts { get; set; }
}
