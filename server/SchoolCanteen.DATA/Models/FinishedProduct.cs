
namespace SchoolCanteen.DATA.Models;

public class FinishedProduct
{
    public int FinishedProductId { get; set; }
    public float Costs { get; set; }
    public float Profit { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    //public List<ProductFinishedProduct> ProductFinishedProducts { get; set; }
    public List<Product> Products { get; set;}
}
