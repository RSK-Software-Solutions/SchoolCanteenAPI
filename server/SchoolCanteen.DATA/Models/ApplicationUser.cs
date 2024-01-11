
using Microsoft.AspNetCore.Identity;

namespace SchoolCanteen.DATA.Models;

public class ApplicationUser : IdentityUser
{
    public Guid CompanyId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}
