
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Repositories.FinishedProductRepo;
using SchoolCanteen.DATA.Repositories.ProductRepo;
using SchoolCanteen.DATA.Repositories.ProductStorageRepo;
using SchoolCanteen.DATA.Repositories.RecipeRepo;
using SchoolCanteen.Logic.DTOs.TotalSummaryDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.Services.FinishedProductServices;

namespace SchoolCanteen.Logic.Services.TotalSummaryServices;

public class TotalSummaryService : ITotalSummaryService
{
    private readonly IMapper mapper;
    private readonly ILogger logger;
    private readonly ITokenUtil tokenUtil;
    private readonly IProductRepository productRepository;
    private readonly IFinishedProductRepository finishedProductRepository;
    private readonly IRecipeRepository recipeRepository;
    private readonly IProductStorageRepository productStorageRepository;

    public TotalSummaryService(IMapper mapper,
        ILogger<FinishedProductService> logger,
        ITokenUtil tokenUtil,
        IProductRepository productRepository,
        IFinishedProductRepository finishedProductRepository,
        IRecipeRepository recipeRepository,
        IProductStorageRepository productStorageRepository)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.tokenUtil = tokenUtil;
        this.productRepository = productRepository;
        this.finishedProductRepository = finishedProductRepository;
        this.recipeRepository = recipeRepository;
        this.productStorageRepository = productStorageRepository;
    }

    public async Task<BasicTotalSummaryDTO> GetTotalSummary()
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var result = new BasicTotalSummaryDTO
            {
                TotalProducts = await productRepository.CountAsync(companyId),
                LowQuantitiesProducts = await productRepository.CountLowAsync(companyId),
                TotalRecipes = await recipeRepository.CountAsync(companyId),
                TotalFinishedProducts = await finishedProductRepository.CountAsync(companyId),
                ExceededDateFinishedProducts = await finishedProductRepository.CountExpiredAsync(companyId)
            };

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
}
