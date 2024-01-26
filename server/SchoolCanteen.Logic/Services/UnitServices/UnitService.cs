
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Repositories.UnitRepo;
using SchoolCanteen.Logic.DTOs.ProductDTOs;
using SchoolCanteen.Logic.DTOs.UnitDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.Services.FinishedProductServices;

namespace SchoolCanteen.Logic.Services.UnitServices;

public class UnitService : IUnitService
{
    private readonly IMapper mapper;
    private readonly ILogger logger;
    private readonly ITokenUtil tokenUtil;
    private IUnitRepository repository;

    public UnitService(
        IMapper mapper,
        ILogger<UnitService> logger,
        ITokenUtil tokenUtil,
        IUnitRepository repository)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.tokenUtil = tokenUtil;
        this.repository = repository;
    }

    public async Task<Unit> CreateAsync(SimpleUnitDto unitDto)
    {
        var companyId = tokenUtil.GetIdentityCompany();

        var existUnit = await repository.GetByNameAsync(unitDto.Name, companyId);
        if (existUnit != null)
        {
            logger.LogInformation($"Unit {existUnit} already exists.");
            return existUnit;
        }

        var newUnit = mapper.Map<Unit>(unitDto);
        await repository.AddAsync(newUnit);

        return newUnit;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var existUnit = await repository.GetByIdAsync(id);
            if (existUnit == null) return false;

            if (tokenUtil.GetIdentityCompany() != existUnit.CompanyId) return false;

            await repository.DeleteAsync(existUnit);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<IEnumerable<SimpleUnitDto>> GetAllAsync()
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();
            var units = await repository.GetAllAsync(companyId);

            return units.Select(unit => mapper.Map<SimpleUnitDto>(unit));
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<Unit> GetByIdAsync(int id)
    {
        try
        {
            var existUnit = await repository.GetByIdAsync(id);
            if (existUnit == null) return null;

            if (tokenUtil.GetIdentityCompany() != existUnit.CompanyId) return null;

            return existUnit;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<Unit> GetByNameAsync(string name)
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();

            var existUnit = await repository.GetByNameAsync(name, companyId);
            if (existUnit == null) return null;

            if (tokenUtil.GetIdentityCompany() != existUnit.CompanyId) return null;

            return existUnit;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public Task<bool> UpdateAsync(SimpleUnitDto unit)
    {
        throw new NotImplementedException();
    }
}
