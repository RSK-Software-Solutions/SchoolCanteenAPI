
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.UnitRepo;
using SchoolCanteen.Logic.DTOs.UnitDTOs;

namespace SchoolCanteen.Logic.Services.UnitServices;

public class UnitBaseService : IUnitBaseService
{
    private readonly IMapper mapper;
    private readonly ILogger logger;
    private IUnitRepository repository;

    public UnitBaseService(
        IMapper mapper,
        ILogger<UnitBaseService> logger,
        IUnitRepository repository)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.repository = repository;
    }
    public async Task<IEnumerable<SimpleUnitDto>> CreateBaseUnitsForCompany(Guid companyId)
    {
        try
        {
            //var companyId = tokenUtil.GetIdentityCompany();
            string[] newUnits = { "szt", "kg", "tona", "litr" };

            foreach (var unitName in newUnits)
            {
                await repository.AddAsync(new Unit { CompanyId = companyId, Name = unitName });
            }
            var units = await repository.GetAllAsync(companyId);
            return units.Select(unit => mapper.Map<SimpleUnitDto>(unit));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }
}
