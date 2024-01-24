using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.FinishedProductRepo;
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
        var productDto = new SimpleFinishedProductDto (0, Guid.NewGuid(), "NewProduct", 1 );
        var repositoryMock = new Mock<IFinishedProductRepository>();
        var tokenUtilMock = new Mock<ITokenUtil>();
        var loggerMock = new Mock<ILogger<FinishedProductService>>();
        var mapperMock = new Mock<IMapper>();

        mapperMock.Setup(mapper => mapper.Map<FinishedProduct>(It.IsAny<SimpleFinishedProductDto>()))
            .Returns(new FinishedProduct { Name = productDto.Name });

        repositoryMock.Setup(repo => repo.GetByNameAsync(productDto.Name))
                      .ReturnsAsync(new FinishedProduct { FinishedProductId = 1, Name = productDto.Name });
        

        var productService = new FinishedProductService(mapperMock.Object, loggerMock.Object, tokenUtilMock.Object, repositoryMock.Object);
        tokenUtilMock.Setup(token => token.GetIdentityCompany()).Returns(productDto.CompanyId);
        // Act
        var result = await productService.CreateAsync(productDto);

        // Assert
        repositoryMock.Verify(repo => repo.GetByNameAsync(productDto.Name), Times.Once);
        repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<FinishedProduct>()), Times.Never);

        Assert.AreEqual(1, result.FinishedProductId);
    }
    [Test]
    public async Task CreateAsync_NonExistingProduct_CreatesNewProduct()
    {
        // Arrange
        var productDto = new SimpleFinishedProductDto(0, Guid.NewGuid(), "NewProduct", 1);
        var repositoryMock = new Mock<IFinishedProductRepository>();
        var tokenUtilMock = new Mock<ITokenUtil>();
        var loggerMock = new Mock<ILogger<FinishedProductService>>();
        var mapperMock = new Mock<IMapper>();

        mapperMock.Setup(mapper => mapper.Map<FinishedProduct>(It.IsAny<SimpleFinishedProductDto>()))
          .Returns(new FinishedProduct { Name = productDto.Name }); 

        repositoryMock.Setup(repo => repo.GetByNameAsync(productDto.Name))
                      .ReturnsAsync((FinishedProduct)null);
        
        tokenUtilMock.Setup(t => t.GetIdentityCompany())
             .Returns(productDto.CompanyId);

        var productService = new FinishedProductService(
            mapperMock.Object, 
            loggerMock.Object, 
            tokenUtilMock.Object, 
            repositoryMock.Object);

        // Act
        var result = await productService.CreateAsync(productDto);

        // Assert
        repositoryMock.Verify(repo => repo.GetByNameAsync(productDto.Name), Times.Once);
        repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<FinishedProduct>()), Times.Once);
        //loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);
        //loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()), Times.Never);
 
        Assert.AreEqual(productDto.Name, result.Name); // Make sure the returned product is the new one.
    }
}