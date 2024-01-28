using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.ProductDTOs;

public class CreateFinishedProductDto
{
    public int RecipeId { get; set; }
    [MaxLength(100)]
    [Required] public string Name { get; set; }
    [Range(0, 1000)]
    public int Quantity { get; set; }
    public int Profit { get; set; }

    public CreateFinishedProductDto(int recipeId, string name, int quantity, int profit)
    {
        RecipeId = recipeId;
        Name = name;
        Quantity = quantity;
        Profit = profit;
    }
}
