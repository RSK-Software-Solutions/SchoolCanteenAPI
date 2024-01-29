
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.DATA.Models;

public class ProductStorage
{
    [Key]
    public int ProductStorageId { get; set; }
    [Required] public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    [Required] public int ProductId { get; set; }
    public Product Product { get; set; }
    public float Price { get; set; } = 0;
    public float Quantity { get; set; } = 0;
    public int ValidityPeriod { get; set; } = 0;
    public int FinishedProductId { get; set; }
    public FinishedProduct FinishedProduct { get; set; }
}
