
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.DATA.Models;

public class ProductStorage
{
    [Key]
    public int ProductStorageId { get; set; }
    [Required] public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public float Price { get; set; } = 0;
    public float Quantity { get; set; } = 0;
    public int ValidityPeriod { get; set; } = 0;

    public List<FinishedProduct> FinishedProducts { get; set; } = new List<FinishedProduct>();
}
