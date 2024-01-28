
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.RecipeRepo;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.DTOs.RecipeDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;

namespace SchoolCanteen.Logic.Services.RecipeServices;

public class RecipeService : IRecipeService
{
    private readonly IMapper mapper;
    private readonly ILogger logger;
    private readonly ITokenUtil tokenUtil;
    private readonly IRecipeRepository repository;
    private readonly IRecipeDetailRepository detailRepository;

    public RecipeService(
        IMapper mapper,
        ILogger<RecipeService> logger,
        ITokenUtil tokenUtil,
        IRecipeRepository repository, 
        IRecipeDetailRepository detailRepository)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.tokenUtil = tokenUtil;
        this.repository = repository;
        this.detailRepository = detailRepository;
    }

    public async Task<SimpleRecipeDto> AddProductToRecipe(CreateRecipeDetailsDto dto)
    {
        var companyId = tokenUtil.GetIdentityCompany();
        var existRecipe = await repository.GetByIdAsync(dto.RecipeId, companyId);

        var newRecipeDetails = mapper.Map<RecipeDetail>(dto);

        existRecipe.Details.Add(newRecipeDetails);
        await repository.UpdateAsync(existRecipe);

        return mapper.Map<SimpleRecipeDto>(existRecipe);
    }

    public async Task<SimpleRecipeDto> CreateAsync(CreateRecipeDto dto)
    {
        var companyId = tokenUtil.GetIdentityCompany();

        var existRecipe = await repository.GetByNameAsync(dto.Name, companyId);
        if (existRecipe != null)
        {
            logger.LogInformation($"Recipe {existRecipe} already exists.");
            return mapper.Map<SimpleRecipeDto>(existRecipe);
        }
        var newRecipe = mapper.Map<Recipe>(dto);
        newRecipe.CompanyId = companyId;

        await repository.AddAsync(newRecipe);

        return mapper.Map<SimpleRecipeDto>(newRecipe);
    }

    public async Task<bool> DeleteAsync(int Id)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existRecipe = await repository.GetByIdAsync(Id, companyId);
            if (existRecipe == null) return false;

            await repository.DeleteAsync(existRecipe);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<bool> DeleteProductFromRecipe(DeleteRecipeDetailsDto dto)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existRecipe = await repository.GetByIdAsync(dto.RecipeId, companyId);
            if (existRecipe == null) return false;

            var existRecipeDetails = await detailRepository.GetByIdAsync(dto.DetailsId);

            var isRecipeContainsDetails =  existRecipe.Details.Contains(existRecipeDetails);
            if (!isRecipeContainsDetails) return false;

            existRecipe.Details.Remove(existRecipeDetails);
            await repository.UpdateAsync(existRecipe);

            return true;
        }
        catch (Exception ex )
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<IEnumerable<SimpleRecipeDto>> GetAllAsync()
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();
            var recipes = await repository.GetAllAsync(companyId);

            return recipes.Select(recipe => mapper.Map<SimpleRecipeDto>(recipe));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<SimpleRecipeDto> GetByIdAsync(int id)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();
            var existRecipe = await repository.GetByIdAsync(id, companyId);

            return mapper.Map<SimpleRecipeDto>(existRecipe);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<bool> UpdateAsync(EditRecipeDto dto)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existRecipe = await repository.GetByIdAsync(dto.RecipeId, companyId);
            if (existRecipe == null) return false;

            mapper.Map(dto, existRecipe);

            await repository.UpdateAsync(existRecipe);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
}
