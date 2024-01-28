
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.ProductDTOs;

namespace SchoolCanteen.Logic.Services.ProductServices;

public interface IProductService
{
    Task<Product> CreateAsync(CreateProductDto product);
    Task<bool> UpdateAsync(EditProductDto product);
    Task<bool> DeleteAsync(int id);
    Task<SimpleProductDto> GetByIdAsync(int id);
    Task<SimpleProductDto> GetByNameAsync(string name);
    Task<IEnumerable<SimpleProductDto>> GetAllAsync();
}
