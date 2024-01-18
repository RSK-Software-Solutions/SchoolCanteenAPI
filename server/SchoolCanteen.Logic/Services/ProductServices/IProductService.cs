
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.ProductDTOs;

namespace SchoolCanteen.Logic.Services.ProductServices;

public interface IProductService
{
    Task<Product> CreateAsync(SimpleProductDto product);
    Task<bool> UpdateAsync(SimpleProductDto product);
    Task<bool> DeleteAsync(int id);
    Task<Product> GetByIdAsync(int id);
    Task<Product> GetByNameAsync(string name);
    Task<IEnumerable<SimpleProductDto>> GetByCompanyIdAsync(Guid companyId);
    Task<IEnumerable<SimpleProductDto>> GetAllAsync();
}
