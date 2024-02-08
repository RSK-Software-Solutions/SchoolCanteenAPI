
using SchoolCanteen.Logic.DTOs.TotalSummaryDTOs;

namespace SchoolCanteen.Logic.Services.TotalSummaryServices;

public interface ITotalSummaryService
{
    Task<BasicTotalSummaryDTO> GetTotalSummary();
}
