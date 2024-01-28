using SchoolCanteen.Logic.DTOs.UnitDTOs;
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.ProductDTOs;

public class SimpleProductDto
{
    public int ProductId { get; set; }
    [Required] public int UnitId { get; set; }
    public  SimpleUnitDto Unit { get; set; }
    [MaxLength(100)]
    [Required] public string Name { get; set; }
    public float Price { get; set; } = 0;

    [Range(0, float.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
    public float Quantity { get; set; } = 0;

    [Range(0, float.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
    public int ValidityPeriod { get; set; } = 0;
    public bool Active { get; set; } = true;

}
