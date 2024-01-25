using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.ProductDTOs;

public class CreateFinishedProductDto
{
    public int FinishedProductId { get; set; }
    //[Required] public Guid CompanyId { get; set; }
    [MaxLength(100)]
    [Required] public string Name { get; set; }
    [Range(0, 1000)]
    public int Quantity { get; set; }

    public CreateFinishedProductDto(int finishedProductId, string name, int quantity)
    {
        FinishedProductId = finishedProductId;
        Name = name;
        Quantity = quantity;
    }
}
