
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.ProductDTOs;

public class ProductForListDto
{
    public int ProductId { get; set; }
    [MaxLength(100)]
    [Required] public string Name { get; set; }
    public float Quantity { get; set; } = 0;
}
