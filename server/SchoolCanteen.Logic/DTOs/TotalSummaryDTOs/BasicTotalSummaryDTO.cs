
namespace SchoolCanteen.Logic.DTOs.TotalSummaryDTOs;

public class BasicTotalSummaryDTO
{
    public int TotalProducts { get; set; }
    public int LowQuantitiesProducts { get; set; }
    public int TotalRecipes { get; set; }
    public int TotalFinishedProducts { get; set; }
    public int ExceededDateFinishedProducts { get; set; }
}
