
using SchoolCanteen.DATA.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.Logic.DTOs.UnitDTOs;

public class SimpleUnitDto
{
    public int UnitId { get; set; }
    [Required] public Guid CompanyId { get; set; }
    [MaxLength(10)]
    [Required] public string Name { get; set; }
}
