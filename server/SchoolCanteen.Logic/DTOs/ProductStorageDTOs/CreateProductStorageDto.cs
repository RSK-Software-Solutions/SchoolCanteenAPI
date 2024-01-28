
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.ProductStorageDTOs;

public class CreateProductStorageDto
{
    [Required] public Guid CompanyId { get; set; }
    [Required] public int ProductId { get; set; }
    public float Price { get; set; } = 0;
    public float Quantity { get; set; } = 0;
    public int ValidityPeriod { get; set; } = 0;
}
