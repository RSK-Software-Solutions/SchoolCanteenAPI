
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.FinishedProductRepo;
using SchoolCanteen.DATA.Repositories.ProductStorageRepo;
using SchoolCanteen.DATA.Repositories.RecipeRepo;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;

namespace SchoolCanteen.Logic.Services.FinishedProductServices;

public class FinishedProductService : IFinishedProductService
{
    private readonly IMapper mapper;
    private readonly ILogger logger;
    private readonly ITokenUtil tokenUtil;
    private readonly IFinishedProductRepository repository;
    private readonly IRecipeRepository recipeRepository;
    private readonly IProductStorageRepository productStorageRepository;

    public FinishedProductService(
        IMapper mapper, 
        ILogger<FinishedProductService> logger,
        ITokenUtil tokenUtil,
        IFinishedProductRepository repository,
        IRecipeRepository recipeRepository,
        IProductStorageRepository productStorageRepository)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.tokenUtil = tokenUtil;
        this.repository = repository;
        this.recipeRepository = recipeRepository;
        this.productStorageRepository = productStorageRepository;
    }

    /// <summary>
    /// Creates a new FinishedProduct asynchronously with the specified name.
    /// If a FinishedProduct with the same name already exists, returns the existing one.
    /// </summary>
    /// <param name="name">The name of the FinishedProduct to be created.</param>
    /// <returns></returns>
    public async Task<SimpleFinishedProductDto> CreateAsync(CreateFinishedProductDto dto)
    {
        var companyId = tokenUtil.GetIdentityCompany();

        var newFinishedProduct = mapper.Map<FinishedProduct>(dto);
        newFinishedProduct.CompanyId = companyId;

        var recipe = await recipeRepository.GetByIdAsync(dto.RecipeId, companyId);

        foreach (var details in recipe.Details)
        {
            var productStorage = new ProductStorage { 
                CompanyId = companyId,
                ProductId = details.ProductId,
                Price = details.Product.Price,
                Quantity = details.Quantity
            };

            details.Product.Quantity -= productStorage.Quantity;

            newFinishedProduct.ProductStorages.Add(productStorage);
        }

        newFinishedProduct.Costs = (float)Math.Round(newFinishedProduct.ProductStorages.Average(x => x.Price), 2);
        newFinishedProduct.Profit = dto.Profit;
        newFinishedProduct.Price = (float)Math.Round(newFinishedProduct.Costs + newFinishedProduct.Costs * newFinishedProduct.Profit / 100, 2);

        var isCreated = await repository.AddAsync(newFinishedProduct);
        if (!isCreated) return null;

        return mapper.Map<SimpleFinishedProductDto>(newFinishedProduct);
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

            var existFinishProduct = await repository.GetByIdAsync(Id, companyId);
            if (existFinishProduct == null) return false;

            List<ProductStorage> listToRemove = new List<ProductStorage>();
            
            foreach (var product in existFinishProduct.ProductStorages)
            {
                product.Product.Quantity += product.Quantity;
                listToRemove.Add(product);
            }
            existFinishProduct.ProductStorages.RemoveAll(item => listToRemove.Contains(item));

            foreach (var item in listToRemove) 
                await productStorageRepository.DeleteAsync(item); 

            await repository.DeleteAsync(existFinishProduct);
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

}
