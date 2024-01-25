
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.FinishedProductRepo;
using SchoolCanteen.DATA.Repositories.ProductRepo;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;

namespace SchoolCanteen.Logic.Services.FinishedProductServices;

public class FinishedProductService : IFinishedProductService
{
    private readonly IMapper mapper;
    private readonly ILogger logger;
    private readonly ITokenUtil tokenUtil;
    private readonly IFinishedProductRepository repository;
    private readonly IProductRepository productRepository;
    private readonly IProductFinishedProductRepository productFnishedProductRepository;

    public FinishedProductService(
        IMapper mapper, 
        ILogger<FinishedProductService> logger,
        ITokenUtil tokenUtil,
        IFinishedProductRepository repository,
        IProductRepository productRepo,
        IProductFinishedProductRepository productFnishedProductRepository)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.tokenUtil = tokenUtil;
        this.repository = repository;
        this.productRepository = productRepo;
        this.productFnishedProductRepository = productFnishedProductRepository;
    }

    /// <summary>
    /// Creates a new FinishedProduct asynchronously with the specified name.
    /// If a FinishedProduct with the same name already exists, returns the existing one.
    /// </summary>
    /// <param name="name">The name of the FinishedProduct to be created.</param>
    /// <returns></returns>
    public async Task<FinishedProduct> CreateAsync(CreateFinishedProductDto dto)
    {
        var companyId = tokenUtil.GetIdentityCompany();

        var existFinishedProduct = await repository.GetByNameAsync(dto.Name, companyId);
        if (existFinishedProduct != null)
        {
            logger.LogInformation($"FinishedProduct {existFinishedProduct} already exists.");
            return existFinishedProduct;
        }

        var newFinishedProduct = mapper.Map<FinishedProduct>(dto);
        newFinishedProduct.CompanyId = companyId;

        await repository.AddAsync(newFinishedProduct);

        return newFinishedProduct;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IEnumerable<SimpleFinishedProductDto>> GetAllAsync()
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();
            var products = await repository.GetAllAsync(companyId);

            return products.Select(product => mapper.Map<SimpleFinishedProductDto>(product));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// Retrieves a collection of FinishedProducts associated with the specified company ID asynchronously.
    /// </summary>
    /// <param name="companyId">The unique identifier of the company whose FinishedProducts are to be retrieved.</param>
    /// <returns></returns>
    /// <exception cref="Exception">
    /// Throws an exception if an error occurs during the retrieval process. The exception is logged with detailed information.
    /// </exception>
    public async Task<IEnumerable<SimpleFinishedProductDto>> GetByCompanyIdAsync(Guid companyId)
    {
        try
        {
            if (tokenUtil.GetIdentityCompany() != companyId) return null;
            var products = await repository.GetAllAsync(companyId);

            return products.Select(product => mapper.Map<SimpleFinishedProductDto>(product));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString() );
        }
    }

    /// <summary>
    /// Retrieves a FinishedProduct by its unique identifier asynchronously.
    /// </summary>
    /// <param name="Id">The unique identifier of the FinishedProduct to be retrieved.</param>
    /// <returns></returns>
    /// <exception cref="Exception">Throws an exception if an error occurs during the retrieval process. The exception is logged with detailed information.</exception>
    public async Task<SimpleFinishedProductDto> GetByIdAsync(int Id)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existProduct =  await repository.GetByIdAsync(Id, companyId);

            return mapper.Map<SimpleFinishedProductDto>(existProduct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// Retrieves a FinishedProduct by its name asynchronously.
    /// </summary>
    /// <param name="finshedProduct">The name of the FinishedProduct to be retrieved.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<FinishedProduct> GetByNameAsync(string finshedProduct)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existProduct = await repository.GetByNameAsync(finshedProduct, companyId);

            return existProduct;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    /// <summary>
    /// Removes a FinishedProduct by its unique identifier asynchronously.
    /// </summary>
    /// <param name="Id">The unique identifier of the FinishedProduct to be removed.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> DeleteAsync(int Id)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existProduct = await repository.GetByIdAsync(Id, companyId);
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
    /// Updates a FinishedProduct asynchronously using the information provided in the specified SimpleFinishedProductDto.
    /// </summary>
    /// <param name="finshedProduct">The SimpleFinishedProductDto containing updated information for the FinishedProduct.</param>
    /// <returns>
    /// Returns a Task representing the asynchronous operation. The task result is a boolean indicating whether the update was successful.
    /// Returns true if the FinishedProduct was successfully updated, or false if the FinishedProduct does not exist or if the calling identity's company
    /// does not match the company associated with the FinishedProduct.
    /// </returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> UpdateAsync(SimpleFinishedProductDto finshedProduct)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existProduct = await repository.GetByIdAsync(finshedProduct.FinishedProductId, companyId);
            if (existProduct == null) return false;

            await repository.UpdateAsync(existProduct);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<AppMessage> AddProductToFinishedProduct(SimpleProductFinishedProductDto dto)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existFinishedProduct = await repository.GetByIdAsync(dto.FinishedProductId, companyId);
            if (existFinishedProduct == null) return new AppMessage(false, "No Finnished Product in database.");

            var existProduct = await productRepository.GetByIdAsync(dto.ProductId);
            if (existProduct == null) return new AppMessage(false, "No Product in database.");

            existFinishedProduct.Products.Add(existProduct);

            await repository.UpdateAsync(existFinishedProduct);

            return new AppMessage(true, "Product hass been added to Finished Product");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<AppMessage> RemoveProductFromFinishedProduct(SimpleProductFinishedProductDto dto)
    {
        throw new NotImplementedException();
    }
}
