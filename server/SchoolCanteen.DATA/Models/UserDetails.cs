
using System.Diagnostics.CodeAnalysis;

namespace SchoolCanteen.DATA.Models;

public class UserDetails
{
    public Guid UserDetailsId { get; set; }
    public Guid UserId { get; set; }

    [AllowNull]
    public string? Street { get; set; }
    [AllowNull]
    public string? Number { get; set; }
    [AllowNull]
    public string? PostalCode { get; set; }
    [AllowNull]
    public string? City { get; set; }
    [AllowNull]
    public string? Email { get; set; }
    [AllowNull]
    public string? Phone { get; set; }
    [AllowNull]
    public User User { get; set; } = null!;
}
