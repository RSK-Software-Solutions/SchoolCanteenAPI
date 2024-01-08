
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolCanteen.DATA.Models.Auth;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;

namespace SchoolCanteen.Logic.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> logger;

        public AuthService(UserManager<IdentityUser> userManager, 
            ITokenService tokenService, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            this.logger = logger;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var managedUser = await _userManager.FindByEmailAsync(email);

            if (managedUser == null) return InvalidEmail(email);

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
            if (!isPasswordValid) return InvalidPassword(email, managedUser.UserName);

            var accessToken = _tokenService.CreateToken(managedUser);

            return new AuthResult(true, managedUser.Email, managedUser.UserName, accessToken);
        }

        public async Task<AuthResult> RegisterAsync(string email, string username, string password)
        {
            var result = await _userManager.CreateAsync(
                new IdentityUser { UserName = username, Email = email }, password);

            if (!result.Succeeded) return FailedRegistration(result, email, username);

            return new AuthResult(true, email, username, "");
        }

        private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
        {
            var authResult = new AuthResult(false, email, username, "");

            foreach (var error in result.Errors) authResult.ErrorMessages.Add(error.Code, error.Description);

            return authResult;
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
