﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.Services.ProductServices;
using System.Data;

namespace SchoolCanteen.API.Controllers;

public class ProductController :ControllerBase
{
    private readonly ILogger<ProductController> logger;
    private readonly IProductService _productService;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        this.logger = logger;
        _productService = productService;
    }

    /// <summary>
    /// Retrieves all Products asynchronously for the authenticated user.
    /// </summary>
    /// <returns>
    /// Returns an ActionResult containing a collection of SimpleProductDto.
    /// If there are no Products found, returns a NotFound result.
    /// If an error occurs during the operation, returns a BadRequest result with the error message.
    /// </returns>
    [HttpGet("/api/product"), Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<SimpleProductDto>>> GetAllAsync()
    {
        try
        {
            var products = await _productService.GetAllAsync();
            if (products.Count() == 0) return NotFound($"No Product found.");

            return Ok(products);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Creates a new Product asynchronously for the authenticated user.
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    [HttpPost("/api/product"), Authorize(Roles = "User")]
    public async Task<ActionResult<bool>> CreateNewAsync([FromBody] CreateProductDto productDto)
    {
        try
        {
            var isCreated = await _productService.CreateAsync(productDto);
            if (isCreated == null) return NotFound("Product not added. You have no rights.");
            return Ok(isCreated);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Updates a Product asynchronously for the authenticated user based on the specified identifier.
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    [HttpPut("/api/product"), Authorize(Roles = "User")]
    public async Task<ActionResult<bool>> UpdateNewAsync([FromBody] EditProductDto productDto)
    {
        try
        {
            var isChanged = await _productService.UpdateAsync(productDto);
            if (isChanged == false) return NotFound("Product not found.");

            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a Product asynchronously for the authenticated user based on the specified identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>
    /// Returns an ActionResult containing a boolean value indicating the success of the deletion.
    /// If the deletion is successful, returns an Ok result.
    /// If the specified Finished Product is not found, returns a NotFound result.
    /// If an error occurs during the deletion, returns a BadRequest result with the error message.
    /// </returns>
    [HttpDelete("/api/product"), Authorize(Roles = "User")]
    public async Task<ActionResult<bool>> DeleteAsync([FromQuery] int id)
    {
        try
        {
            var isDeleted = await _productService.DeleteAsync(id);
            if (isDeleted == false) return NotFound("Product not found.");

            return Ok(isDeleted);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            return BadRequest(ex.Message);
        }
    }
}
