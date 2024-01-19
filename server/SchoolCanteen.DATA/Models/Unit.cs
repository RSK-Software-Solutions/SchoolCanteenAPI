
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.DATA.Models;

public class Unit
{
    public int UnitId { get; set; }
    [Required] public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    [MaxLength(10)]
    [Required] public string Name { get; set; }
    public Product Product { get; set; }
    public RecipeDetail Details { get; set; }
}
