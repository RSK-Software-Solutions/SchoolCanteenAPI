using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SchoolCanteen.DATA.DatabaseConnector;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.RecipeRepo;

namespace SchoolCanteen.Test;

public class RecipeRepositoryTest
{
    private DatabaseApiContext dbContext;
    private Mock<DatabaseApiContext> mockContext;
    private Mock<ILogger<RecipeRepository>> logger;
    private RecipeRepository recipeRepository;

    [SetUp]
    public void Setup()
    {
        var dbContextOptions = new DbContextOptionsBuilder<DatabaseApiContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDb")
            .Options;

        dbContext = new DatabaseApiContext(dbContextOptions);
        logger = new Mock<ILogger<RecipeRepository>>();

        var CompanyId = Guid.NewGuid();
        var recipes = new List<Recipe>
        {
            new Recipe { RecipeId = 1, Name = "Receptura 1", CompanyId = CompanyId, UnitId = 1, Quantity = 1 },
            new Recipe { RecipeId = 2, Name = "Receptura 2", CompanyId = CompanyId, UnitId = 1, Quantity = 10 }
        };

        dbContext.Recipes.AddRange(recipes);
        dbContext.SaveChanges();

        recipeRepository = new RecipeRepository(dbContext, logger.Object);
    }

    [Test]
    public async Task GetByIdAsync_WithExistingId_ReturnsRecipe()
    {
        // Act
        var companyId = Guid.NewGuid();
        var result = await recipeRepository.GetByIdAsync(2, companyId);

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(2, result.RecipeId);
        // Add more assertions based on your specific requirements
    }
}