
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.DATA.Models.Auth;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using SchoolCanteen.Logic.Services.CompanyServices;

namespace SchoolCanteen.Logic.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly ICompanyService _companyService;
        private readonly ILogger<AuthService> logger;

        public AuthService(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService, 
            ICompanyService companyService, 
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _companyService = companyService;
            this.logger = logger;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var managedUser = await _userManager.FindByEmailAsync(email);

            if (managedUser == null) return InvalidEmail(email);

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
            if (!isPasswordValid) return InvalidPassword(email, managedUser.UserName);

            var roles = await _userManager.GetRolesAsync(managedUser);

            var accessToken = _tokenService.CreateToken(managedUser, roles);

            return new AuthResult(true, managedUser.Email, managedUser.UserName, managedUser.CompanyId, accessToken);
        }
        public async Task<AuthResult> RegisterAsync(string email, string username, string password, string role, string companyName)
        {
            var company = await _companyService.GetCompanyByNameAsync(companyName);
            if (company == null)
            {
                company = await _companyService.CreateCompanyAsync(companyName);
            }

            var newUser = new ApplicationUser { UserName = username, Email = email, CompanyId = company.CompanyId };
            var result = await _userManager.CreateAsync(newUser, password);

            if (!result.Succeeded) return FailedRegistration(result, email, username);

            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists) return FailedRoleNotExists(role);

            if (!await IsAdminUserInCompany(company.CompanyId))
                await _userManager.AddToRoleAsync(newUser, "Admin");

            await _userManager.AddToRoleAsync(newUser, role);

            return new AuthResult(true, email, username, newUser.CompanyId, "");
        }
        private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
        {
            var authResult = new AuthResult(false, email, username, Guid.Empty, "");

            foreach (var error in result.Errors) authResult.ErrorMessages.Add(error.Code, error.Description);

            return authResult;
        }
        private static AuthResult FailedRoleNotExists(string roleName)
        {
            var result = new AuthResult(false, "", "", Guid.Empty, "");
            result.ErrorMessages.Add("Bad role Name", $"Role {roleName} not exists");
            return result;
        }
        private static AuthResult InvalidPassword(string email, string userName)
        {
            var result = new AuthResult(false, email, userName, Guid.Empty, "");
            result.ErrorMessages.Add("Bad credentials", "Invalid password or email");
            return result;
        }
        private static AuthResult InvalidEmail(string email)
        {
            var result = new AuthResult(false, email, "", Guid.Empty, "");
            result.ErrorMessages.Add("Bad credentials", "Invalid password or email");
            return result;
        }
        private  async Task<bool> IsAdminUserInCompany(Guid companyId)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync("Admin");
            return usersInRole.Any(user => user.CompanyId == companyId);
        }
    }

}
