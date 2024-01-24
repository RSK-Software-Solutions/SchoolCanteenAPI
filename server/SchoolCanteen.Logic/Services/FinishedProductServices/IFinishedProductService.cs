
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.ProductDTOs;

namespace SchoolCanteen.Logic.Services.FinishedProductServices;

public interface IFinishedProductService
{
    Task<FinishedProduct> CreateAsync(SimpleFinishedProductDto dto);
    Task<bool> UpdateAsync(SimpleFinishedProductDto finshedProduct);
    Task<bool> DeleteAsync(int Id);
    Task<FinishedProduct> GetByNameAsync(string finshedProduct);
    Task<SimpleFinishedProductDto> GetByIdAsync(int Id);
    Task<IEnumerable<SimpleFinishedProductDto>> GetByCompanyIdAsync(Guid companyId);
    Task<IEnumerable<SimpleFinishedProductDto>> GetAllAsync();
}
