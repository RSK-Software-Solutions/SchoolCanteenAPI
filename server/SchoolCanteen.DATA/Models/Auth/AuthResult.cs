
namespace SchoolCanteen.DATA.Models.Auth;

public record AuthResult(
    bool Success,
    string Email,
    string UserName,
    Guid ComanyId,
    string Token
    )
{
    public readonly Dictionary<string, string> ErrorMessages = new();
}
