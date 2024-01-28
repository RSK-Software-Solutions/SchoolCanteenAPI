
using SchoolCanteen.DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.ProductDTOs;

public class SimpleFinishedProductDto
{
    public int FinishedProductId { get; set; }

    [MaxLength(100)]
    [Required] public string Name { get; set; }
    [Range(0, 1000)]
    public int Quantity { get; set; }
    public float Costs { get; set; } 
    public float Profit { get; set; } 
    public float Price { get; set; }
    public IEnumerable<ProductForListDto> Products { get; set; }

    public SimpleFinishedProductDto()
    {
    }
    public SimpleFinishedProductDto(int finishedProductId, Guid companyId, string name, int quantity, IEnumerable<ProductForListDto> products)
    {
        FinishedProductId = finishedProductId;
        //CompanyId = companyId;
        Name = name;
        Quantity = quantity;
        Products = products;
    }
}
