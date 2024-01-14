
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.FinishedProductRepo;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.Services.CompanyServices;
using System.ComponentModel.Design;

namespace SchoolCanteen.Logic.Services.FinishedProductServices;

public class FinishedProductService : IFinishedProductService
{
    private readonly IMapper mapper;
    private readonly ILogger logger;
    private readonly ITokenUtil tokenUtil;
    private readonly IFinishedProductRepository repository;

    public FinishedProductService(
        IMapper mapper, 
        ILogger<FinishedProductService> logger,
        ITokenUtil tokenUtil,
        IFinishedProductRepository repository)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.tokenUtil = tokenUtil;
        this.repository = repository;
    }

    /// <summary>
    /// Creates a new FinishedProduct asynchronously with the specified name.
    /// If a FinishedProduct with the same name already exists, returns the existing one.
    /// </summary>
    /// <param name="name">The name of the FinishedProduct to be created.</param>
    /// <returns></returns>
    public async Task<FinishedProduct> CreateAsync(SimpleFinishedProductDto dto)
    {
        if (tokenUtil.GetIdentityCompany() != dto.CompanyId) return null;

        var existFinishedProduct = await repository.GetByNameAsync(dto.Name);
        if (existFinishedProduct != null)
        {
            logger.LogInformation($"FinishedProduct {existFinishedProduct} already exists.");
            return existFinishedProduct;
        }

        var newFinishedProduct = mapper.Map<FinishedProduct>(dto);
        await repository.AddAsync(newFinishedProduct);

        return newFinishedProduct;
    }
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
    public async Task<FinishedProduct> GetByIdAsync(int Id)
    {
        try
        {
            var existProduct =  await repository.GetByIdAsync(Id);
            if (existProduct == null) return null;

            if (tokenUtil.GetIdentityCompany() != existProduct.CompanyId) return null;

            return existProduct;
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
            var existProduct = await repository.GetByNameAsync(finshedProduct);
            if (existProduct == null) return null;

            if (tokenUtil.GetIdentityCompany() != existProduct.CompanyId) return null;

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
    public async Task<bool> RemoveAsync(int Id)
    {
        try
        {
            var existProduct = await repository.GetByIdAsync(Id);
            if (existProduct == null) return false;

            if (tokenUtil.GetIdentityCompany() != existProduct.CompanyId) return false;

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
            var existProduct = await repository.GetByIdAsync(finshedProduct.FinishedProductId);
            if (existProduct == null) return false;

            if (tokenUtil.GetIdentityCompany() != existProduct.CompanyId) return false;

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
