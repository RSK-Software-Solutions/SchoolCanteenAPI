﻿
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
        ILogger<FinishedProductService> logger,
        ITokenUtil tokenUtil,
        IProductRepository repository)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.tokenUtil = tokenUtil;
        this.repository = repository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<Product> CreateAsync(SimpleProductDto dto)
    {
        if (tokenUtil.GetIdentityCompany() != dto.CompanyId) return null;

        var existProduct = await repository.GetByNameAsync(dto.Name);
        if (existProduct != null)
        {
            logger.LogInformation($"Product {existProduct} already exists.");
            return existProduct;
        }

        var newProduct = mapper.Map<Product>(dto);
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
            var existProduct = await repository.GetByIdAsync(id);
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
    /// <param name="companyId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<IEnumerable<SimpleProductDto>> GetByCompanyIdAsync(Guid companyId)
    {
        try
        {
            if (tokenUtil.GetIdentityCompany() != companyId) return null;
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
    public async Task<Product> GetByIdAsync(int id)
    {
        try
        {
            var existProduct = await repository.GetByIdAsync(id);
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
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Product> GetByNameAsync(string name)
    {
        try
        {
            var existProduct = await repository.GetByNameAsync(name);
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
    /// 
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> UpdateAsync(SimpleProductDto productDto)
    {
        try
        {
            var existProduct = await repository.GetByIdAsync(productDto.ProductId);
            if (existProduct == null) return false;

            if (tokenUtil.GetIdentityCompany() != productDto.CompanyId) return false;

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
