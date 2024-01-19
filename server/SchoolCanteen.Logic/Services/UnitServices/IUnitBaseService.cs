
using SchoolCanteen.Logic.DTOs.UnitDTOs;

namespace SchoolCanteen.Logic.Services.UnitServices;

public interface IUnitBaseService
{
    Task<IEnumerable<SimpleUnitDto>> CreateBaseUnitsForCompany(Guid companyId);
}
