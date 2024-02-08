
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.ProductDTOs;

namespace SchoolCanteen.Logic.Services.FinishedProductServices;

public interface IFinishedProductService
{
    Task<SimpleFinishedProductDto> DecreaseQuantityAsync(int productId, int quantity);
    Task<SimpleFinishedProductDto> CreateAsync(CreateFinishedProductDto dto);
    Task<bool> UpdateAsync(SimpleFinishedProductDto finshedProduct);
    Task<bool> DeleteAsync(int Id);
    Task<FinishedProduct> GetByNameAsync(string finshedProduct);
    Task<SimpleFinishedProductDto> GetByIdAsync(int Id);
    Task<IEnumerable<SimpleFinishedProductDto>> GetByCompanyIdAsync(Guid companyId);
    Task<IEnumerable<SimpleFinishedProductDto>> GetAllAsync();

}
