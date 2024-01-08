
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models.Auth;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;

namespace SchoolCanteen.Logic.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> logger;

        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
        ITokenService tokenService, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
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

            return new AuthResult(true, managedUser.Email, managedUser.UserName, accessToken);
        }

        public async Task<AuthResult> RegisterAsync(string email, string username, string password, string role)
        {
            var user = new IdentityUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded) return FailedRegistration(result, email, username);

            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists) return FailedRoleNotExists(role);

            await _userManager.AddToRoleAsync(user, role);

            return new AuthResult(true, email, username, "");
        }

        private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
        {
            var authResult = new AuthResult(false, email, username, "");

            foreach (var error in result.Errors) authResult.ErrorMessages.Add(error.Code, error.Description);

            return authResult;
        }
        private static AuthResult FailedRoleNotExists(string roleName)
        {
            var result = new AuthResult(false, "", "", "");
            result.ErrorMessages.Add("Bad role Name", $"Role {roleName} not exists");
            return result;
        }
        private static AuthResult InvalidPassword(string email, string userName)
        {
            var result = new AuthResult(false, email, userName, "");
            result.ErrorMessages.Add("Bad credentials", "Invalid password or email");
            return result;
        }

        private static AuthResult InvalidEmail(string email)
        {
            var result = new AuthResult(false, email, "", "");
            result.ErrorMessages.Add("Bad credentials", "Invalid password or email");
            return result;
        }
    }

}
