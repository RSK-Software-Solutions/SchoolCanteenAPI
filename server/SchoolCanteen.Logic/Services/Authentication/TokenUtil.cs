
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace SchoolCanteen.Logic.Services.Authentication;

public class TokenUtil
{
    private readonly HttpContext _context;

    public static IEnumerable<string> GetUserIdByToken(HttpHeaders headers, string secret)
    {
        var token = headers.GetValues("NameIdentifier");
        return token;
    }
    //private static async Task<bool> IsUserMachWithCompany(Guid CompanyId)
    //{
    //    var token = _context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

    //    var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    //    var user = await _userManager.FindByIdAsync(userId);
    //    if (user.CompanyId != CompanyId) return false;
    //    return true;
    //}
}
