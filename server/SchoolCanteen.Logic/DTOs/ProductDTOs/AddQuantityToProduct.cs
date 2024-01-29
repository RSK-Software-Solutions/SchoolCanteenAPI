
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.ProductDTOs;

public class AddQuantityToProduct
{
    [Required] public float Quantity { get; set; }
}
