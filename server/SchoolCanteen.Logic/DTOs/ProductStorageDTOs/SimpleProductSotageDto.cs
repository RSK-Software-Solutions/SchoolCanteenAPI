
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.ProductStorageDTOs;

public class SimpleProductSotageDto
{
    public int ProductStorageId { get; set; }
    public int ProductId { get; set; }
    public ProductForListDto Product { get; set; }
    public float Price { get; set; } = 0;
    public float Quantity { get; set; } = 0;
    public int ValidityPeriod { get; set; } = 0;

}
