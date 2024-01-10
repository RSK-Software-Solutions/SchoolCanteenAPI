
using Microsoft.AspNetCore.Identity;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.Services.Authentication.Interfaces;

public interface ITokenService
{
    public string CreateToken(ApplicationUser user, IEnumerable<string> roles);
}
