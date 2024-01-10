
namespace SchoolCanteen.DATA.Models;

public class ProductFinishedProduct
{
    public int ProductId { get; set; }
    public int FinishedProductId { get; set; }
    public Product Product { get; set; } = null!;
    public FinishedProduct FinishedProduct { get; set; } = null!;
}
