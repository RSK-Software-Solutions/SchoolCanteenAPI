
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.ProductRepo;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.Services.FinishedProductServices;

namespace SchoolCanteen.Logic.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IMapper mapper;
    private readonly ILogger logger;
    private readonly ITokenUtil tokenUtil;
    private readonly IProductRepository repository;

    public ProductService(
        IMapper mapper,
        ILogger<ProductService> logger,
        ITokenUtil tokenUtil,
        IProductRepository repository)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.tokenUtil = tokenUtil;
        this.repository = repository;
    }

    public async Task<SimpleProductDto> IncreaseQuantityAsync(int productId, float quantity)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existProduct = await repository.GetByIdAsync(productId, companyId);
            if (existProduct == null) return null;

            existProduct.Quantity += quantity;
            repository.UpdateAsync(existProduct);

            return mapper.Map<SimpleProductDto>(existProduct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<SimpleProductDto> DecreaseQuantityAsync(int productId, float quantity)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existProduct = await repository.GetByIdAsync(productId, companyId);
            if (existProduct == null) return null;

            if (existProduct.Quantity < quantity) return null;

            existProduct.Quantity -= quantity;
            repository.UpdateAsync(existProduct);

            return mapper.Map<SimpleProductDto>(existProduct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<Product> CreateAsync(CreateProductDto dto)
    {
        var companyId = tokenUtil.GetIdentityCompany();

        var existProduct = await repository.GetByNameAsync(dto.Name, companyId);
        if (existProduct != null)
        {
            logger.LogInformation($"Product {existProduct} already exists.");
            return existProduct;
        }

        var newProduct = mapper.Map<Product>(dto);
        newProduct.CompanyId = companyId;
        await repository.AddAsync(newProduct);

        return newProduct;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existProduct = await repository.GetByIdAsync(id, companyId);
            if (existProduct == null) return false;

            await repository.DeleteAsync(existProduct);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IEnumerable<SimpleProductDto>> GetAllAsync()
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();
            var products = await repository.GetAllAsync(companyId);

            return products.Select(product => mapper.Map<SimpleProductDto>(product));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<SimpleProductDto> GetByIdAsync(int id)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existProduct = await repository.GetByIdAsync(id, companyId);
            if (existProduct == null) return null;



            return mapper.Map<SimpleProductDto>(existProduct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<SimpleProductDto> GetByNameAsync(string name)
    {
        try
        {
            var existProduct = await repository.GetByNameAsync(name, tokenUtil.GetIdentityCompany());

            return mapper.Map< SimpleProductDto>(existProduct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<IEnumerable<SimpleProductDto>> GetListByNameAsync(string name)
    {
        try
        {
            var existProducts = String.IsNullOrEmpty(name)
                ? await repository.GetAllAsync(tokenUtil.GetIdentityCompany())
                : await repository.GetListByNameAsync(name, tokenUtil.GetIdentityCompany());

            return existProducts.Select(product => mapper.Map<SimpleProductDto>(product));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> UpdateAsync(EditProductDto productDto)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existProduct = await repository.GetByIdAsync(productDto.ProductId, companyId);
            if (existProduct == null) return false;

            mapper.Map(productDto, existProduct);

            await repository.UpdateAsync(existProduct);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
}
