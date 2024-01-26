
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.RecipeRepo;
using SchoolCanteen.Logic.DTOs.RecipeDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using System.ComponentModel.Design;

namespace SchoolCanteen.Logic.Services.RecipeServices;

public class RecipeDetailsService : IRecipeDetailsService
{
    private readonly IMapper mapper;
    private readonly ILogger logger;
    private readonly ITokenUtil tokenUtil;
    private readonly IRecipeRepository repositoryRecipe;
    private readonly IRecipeDetailRepository repository;

    public RecipeDetailsService(
        IMapper mapper,
        ILogger<RecipeDetailsService> logger,
        ITokenUtil tokenUtil,
        IRecipeDetailRepository repository,
        IRecipeRepository repositoryRecipe)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.tokenUtil = tokenUtil;
        this.repository = repository;
        this.repositoryRecipe = repositoryRecipe;
    }
    public async Task<SimpleRecipeDetailsDto> CreateAsync(CreateRecipeDetailsDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SimpleRecipeDetailsDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
