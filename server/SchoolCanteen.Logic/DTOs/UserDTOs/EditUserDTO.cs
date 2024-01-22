
using Microsoft.AspNetCore.Identity;
using SchoolCanteen.DATA.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SchoolCanteen.Logic.DTOs.UserDTOs;

public class EditUserDTO
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [AllowNull]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [AllowNull]
    [MaxLength(100)]
    public string LastName { get; set; }
    [AllowNull]
    [MaxLength(100)]
    public string Street { get; set; }
    [AllowNull]
    [MaxLength(10)]
    public string? PostalCode { get; set; }
    [AllowNull]
    [MaxLength(100)]
    public string City { get; set; }
    [AllowNull]
    [MaxLength(100)]
    public string State { get; set; }
    [AllowNull]
    [MaxLength(100)]
    public string Country { get; set; } = string.Empty;
    public List<string> Roles { get; set; }

}
