
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.ProductDTOs;

namespace SchoolCanteen.Logic.Services.FinishedProductServices;

public interface IFinishedProductService
{
    Task<FinishedProduct> CreateAsync(CreateFinishedProductDto dto);
    Task<bool> UpdateAsync(SimpleFinishedProductDto finshedProduct);
    Task<bool> DeleteAsync(int Id);
    Task<FinishedProduct> GetByNameAsync(string finshedProduct);
    Task<SimpleFinishedProductDto> GetByIdAsync(int Id);
    Task<IEnumerable<SimpleFinishedProductDto>> GetByCompanyIdAsync(Guid companyId);
    Task<IEnumerable<SimpleFinishedProductDto>> GetAllAsync();
    Task<AppMessage> AddProductToFinishedProduct(SimpleProductFinishedProductDto dto);
    Task<AppMessage> RemoveProductFromFinishedProduct(SimpleProductFinishedProductDto dto);
}
