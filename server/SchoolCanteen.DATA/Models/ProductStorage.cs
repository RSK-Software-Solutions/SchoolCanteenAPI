
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.DATA.Models;

public class ProductStorage
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public float Price { get; set; } = 0;
    public float Quantity { get; set; } = 0;
    public int ValidityPeriod { get; set; } = 0;
}
