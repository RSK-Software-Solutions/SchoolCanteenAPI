
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.Services.User;
using System.Data;


namespace SchoolCanteen.Logic.Services.UserServices;

public class UserService : IUserService
{
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly ITokenUtil tokenUtil;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public UserService(ILogger<UserService> logger, IMapper mapper, ITokenUtil tokenUtil, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.logger = logger;
        this.userManager = userManager;
        this.mapper = mapper;
        this.tokenUtil = tokenUtil;
        this.roleManager = roleManager;

    }

    public async Task<IdentityResult> CreateAsync(CreateUserDTO userDto)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(userDto.Email);
            if (user != null) return null;

            var companyId = tokenUtil.GetIdentityCompany();
            
            var roleExists = await roleManager.RoleExistsAsync(userDto.RoleName);
            if (roleExists)
            {
                var newUser = new ApplicationUser { UserName = userDto.UserName,  Email = userDto.Email, CompanyId = companyId };

                var result = await userManager.CreateAsync(newUser, userDto.Password);

                if (!result.Succeeded) return null;

                await userManager.AddToRoleAsync(newUser, userDto.RoleName);
                return result;
            }
            return new IdentityResult();

        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<IdentityResult> DeleteAsync(Guid Id)
    {
        try
        {
            var user = await userManager.FindByIdAsync(Id.ToString());
            if (user == null) return null;

            var companyId = tokenUtil.GetIdentityCompany();
            if (user.CompanyId != companyId) return null;

            return await userManager.DeleteAsync(user);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<IEnumerable<SimpleUserDTO>> GetAllByCompanyAsync()
    {
        try
        {
            var companyId = tokenUtil.GetIdentityCompany();
            var users = await userManager.Users
                .Where(e => e.CompanyId == companyId)
                .ToListAsync();
            if (users.Count() == 0) return new List<SimpleUserDTO>();

            return ConvertUserToDto(users);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<SimpleUserDTO> GetByNameAsync(string userLogin)
    {
        try
        {
            var user = await userManager.FindByNameAsync(userLogin);
            if (user == null) return null;

            var companyId = tokenUtil.GetIdentityCompany();
            if (user.CompanyId != companyId) return null;

            var result = mapper.Map<SimpleUserDTO>(user);

            result.Roles.AddRange(await userManager.GetRolesAsync(user));

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public async Task<IdentityResult> UpdateAsync(EditUserDTO userDto)
    {
        try
        {
            var user = await userManager.FindByIdAsync(userDto.Id.ToString());
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            if (!await IsRolesExists(userDto.Roles))
                return IdentityResult.Failed(new IdentityError { Description = "Roles do not exist." });

            // Dodać walidację, czy użytkownik z tokena jest administratorem lub czy użytkownik z tokena to użytkownik z DTO (czyli zmienia sam siebie)

            // W przypadku braku walidacji, mapujemy dane z DTO na obiekt użytkownika
            mapper.Map(userDto, user);

            var result = await userManager.UpdateAsync(user);
            await userManager.AddToRolesAsync(user, userDto.Roles);

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw new Exception(ex.ToString());
        }
    }

    public virtual async Task<bool> IsRolesExists(IEnumerable<string> roles)
    {
        foreach (var role in roles)
        {
            var existRole = await roleManager.FindByNameAsync(role);
            if (existRole == null) return false;
        }
        return true;
    }

    private IEnumerable<SimpleUserDTO> ConvertUserToDto(IEnumerable<ApplicationUser> users)
    {
        var result = users.Select(async user =>
        {
            var roles = await userManager.GetRolesAsync(user);
            var simpleUserDto = mapper.Map<SimpleUserDTO>(user);
            simpleUserDto.Roles.AddRange(roles);
            return simpleUserDto;
        }).Select(task => task.Result);

        return result;
    }

}
