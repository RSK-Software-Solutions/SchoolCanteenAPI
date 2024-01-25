using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.FinishedProductRepo;
using SchoolCanteen.DATA.Repositories.ProductRepo;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.Services.FinishedProductServices;

namespace SchoolCanteen.Test;

[TestFixture]
public class FinishedProductServiceTest
{
    [Test]
    public async Task CreateAsync_ExistingProduct_ReturnsExistingProduct()
    {
        // Arrange
        var companyId = Guid.NewGuid();
        var productDto = new SimpleFinishedProductDto(0, companyId, "NewProduct", 1, new List<ProductForListDto>());
        var createproductDto = new CreateFinishedProductDto(0, "NewProduct", 1);
        var repositoryMock = new Mock<IFinishedProductRepository>();
        var repositoryProductMock = new Mock<IProductRepository>();
        var repositoryProdFinishProdMock = new Mock<IProductFinishedProductRepository>();
        var tokenUtilMock = new Mock<ITokenUtil>();
        var loggerMock = new Mock<ILogger<FinishedProductService>>();
        var mapperMock = new Mock<IMapper>();

        mapperMock.Setup(mapper => mapper.Map<FinishedProduct>(It.IsAny<SimpleFinishedProductDto>()))
            .Returns(new FinishedProduct { Name = productDto.Name });

        repositoryMock.Setup(repo => repo.GetByNameAsync(productDto.Name, companyId))
                      .ReturnsAsync(new FinishedProduct { FinishedProductId = 1, Name = productDto.Name });

        var productService = new FinishedProductService
            (mapperMock.Object, 
            loggerMock.Object, 
            tokenUtilMock.Object, 
            repositoryMock.Object,
            repositoryProductMock.Object,
            repositoryProdFinishProdMock.Object);

        tokenUtilMock.Setup(token => token.GetIdentityCompany()).Returns(companyId);
        //loggerMock.Setup(logger => logger.LogInformation($"FinishedProduct {productDto} already exists."));
        // Act
        var result = await productService.CreateAsync(createproductDto);

        // Assert
        repositoryMock.Verify(repo => repo.GetByNameAsync(productDto.Name, companyId), Times.Once);
        repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<FinishedProduct>()), Times.Never);
        //loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);

        Assert.AreEqual(1, result.FinishedProductId);
    }
    [Test]
    public async Task CreateAsync_NonExistingProduct_CreatesNewProduct()
    {
        // Arrange
        var companyId = Guid.NewGuid();
        var productDto = new SimpleFinishedProductDto(0, Guid.NewGuid(), "NewProduct", 1, new List<ProductForListDto>());
        var createproductDto = new CreateFinishedProductDto(0, "NewProduct", 1);
        var repositoryMock = new Mock<IFinishedProductRepository>();
        var tokenUtilMock = new Mock<ITokenUtil>();
        var repositoryProductMock = new Mock<IProductRepository>();
        var repositoryProdFinishProdMock = new Mock<IProductFinishedProductRepository>();
        var loggerMock = new Mock<ILogger<FinishedProductService>>();
        var mapperMock = new Mock<IMapper>();

        mapperMock.Setup(mapper => mapper.Map<FinishedProduct>(It.IsAny<SimpleFinishedProductDto>()))
          .Returns(new FinishedProduct { Name = productDto.Name }); 

        repositoryMock.Setup(repo => repo.GetByNameAsync(productDto.Name, companyId))
                      .ReturnsAsync((FinishedProduct)null);
        
        tokenUtilMock.Setup(t => t.GetIdentityCompany())
             .Returns(companyId);

        var productService = new FinishedProductService
            (mapperMock.Object,
            loggerMock.Object,
            tokenUtilMock.Object,
            repositoryMock.Object,
            repositoryProductMock.Object,
            repositoryProdFinishProdMock.Object);

        // Act
        var result = await productService.CreateAsync(createproductDto);

        // Assert
        repositoryMock.Verify(repo => repo.GetByNameAsync(productDto.Name, companyId), Times.Once);
        repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<FinishedProduct>()), Times.Once);
        //loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);
        //loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
 
        Assert.AreEqual(productDto.Name, result.Name); // Make sure the returned product is the new one.
    }
}