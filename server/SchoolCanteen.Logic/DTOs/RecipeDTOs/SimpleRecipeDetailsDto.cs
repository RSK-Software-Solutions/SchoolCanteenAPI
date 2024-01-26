
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.DTOs.UnitDTOs;
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.RecipeDTOs;

public class SimpleRecipeDetailsDto
{
    //public Recipe Recipe { get; set; }
    public int ProductId { get; set; }
    public SimpleProductDto Product { get; set; }
    public int UnitId { get; set; }
    public SimpleUnitDto Unit { get; set; }
    public int Quantity { get; set; } = 0;
}
