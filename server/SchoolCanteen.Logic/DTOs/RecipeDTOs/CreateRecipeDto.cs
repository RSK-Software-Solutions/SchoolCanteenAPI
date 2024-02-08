
using SchoolCanteen.DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.RecipeDTOs;

public class CreateRecipeDto
{
    [MaxLength(100)]
    [Required] public string Name { get; set; }
    [Required] public int UnitId { get; set; }
    public float Quantity { get; set; } = 0;
    public int ValidityPeriod { get; set; }
}
