using SchoolCanteen.Logic.DTOs.ProductStorageDTOs;

namespace SchoolCanteen.Logic.DTOs.ProductDTOs;

public class SimpleFinishedProductDto
{
    public int FinishedProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public float Costs { get; set; } 
    public float Profit { get; set; } 
    public float Price { get; set; }
    public List<SimpleProductSotageDto> ProductStorages { get; set; } 

    public SimpleFinishedProductDto()
    {
    }
    public SimpleFinishedProductDto(int finishedProductId, Guid companyId, string name, int quantity, List<SimpleProductSotageDto> products)
    {
        FinishedProductId = finishedProductId;
        //CompanyId = companyId;
        Name = name;
        Quantity = quantity;
        ProductStorages = products;
    }
}
