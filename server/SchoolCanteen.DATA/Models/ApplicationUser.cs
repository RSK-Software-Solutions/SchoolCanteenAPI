
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SchoolCanteen.DATA.Models;

public class ApplicationUser : IdentityUser
{
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }

    [AllowNull]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [AllowNull]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    [AllowNull]
    [MaxLength(100)]
    public string Street { get; set; } = string.Empty;
    [AllowNull]
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;
    [AllowNull]
    [MaxLength(100)]
    public string State { get; set; } = string.Empty;
    [AllowNull]
    [MaxLength(10)]
    public string? PostalCode { get; set; }
    [AllowNull]
    [MaxLength(100)]
    public string Country { get; set; } = string.Empty;
}
