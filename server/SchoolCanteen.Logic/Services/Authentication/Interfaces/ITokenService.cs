
using Microsoft.AspNetCore.Identity;

namespace SchoolCanteen.Logic.Services.Authentication.Interfaces;

public interface ITokenService
{
    public string CreateToken(IdentityUser user, IEnumerable<string> roles);
}
