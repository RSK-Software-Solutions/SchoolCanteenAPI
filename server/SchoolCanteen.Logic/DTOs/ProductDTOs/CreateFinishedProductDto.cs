using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.ProductDTOs;

public class CreateFinishedProductDto
{
    public int RecipeId { get; set; }

    [Range(0, 1000)]
    public int Quantity { get; set; }
    public int Profit { get; set; }

    public CreateFinishedProductDto(int recipeId, int quantity, int profit)
    {
        RecipeId = recipeId;
        Quantity = quantity;
        Profit = profit;
    }
}
