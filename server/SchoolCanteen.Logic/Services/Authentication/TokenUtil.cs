
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.Services.Authentication.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace SchoolCanteen.Logic.Services.Authentication;

public class TokenUtil : ITokenUtil
{
    private readonly IHttpContextAccessor context;
    private readonly UserManager<ApplicationUser> userManager;
    public Guid IdentityCompany { get; private set; }
    public TokenUtil(UserManager<ApplicationUser> userManager, IHttpContextAccessor context)
    {
        this.userManager = userManager;
        this.context = context;
        IdentityCompany = SetCompanyIdByTokenAsync().Result;
    }

    public Guid GetIdentityCompany()
    {
        return IdentityCompany;
    }
    private async Task<Guid> SetCompanyIdByTokenAsync()
    {
        var jsonToken = GetToken();

        var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = await userManager.FindByIdAsync(userId);
        if (IdentityCompany == Guid.Empty) IdentityCompany = user.CompanyId;

        return IdentityCompany;
    }
    private JwtSecurityToken GetToken()
    {
        var httpContext = context.HttpContext ?? throw new InvalidOperationException("HttpContext not available");

        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.ReadToken(token) as JwtSecurityToken;
    }
    private async Task<bool> IsUserMatchedWithCompanyAsync(Guid CompanyId)
    {
        var jsonToken = GetToken();
        if (jsonToken == null) return false;

        var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var user = await userManager.FindByIdAsync(userId);
        if (user.CompanyId != CompanyId) return false;
        return true;
    }

}
