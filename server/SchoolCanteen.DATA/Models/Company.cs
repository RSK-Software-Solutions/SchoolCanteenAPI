
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SchoolCanteen.DATA.Models;

public class Company
{
    public Guid CompanyId { get; set; }
    [Required]
    public string Name { get; set;}
    public int Nip { get; set;}
    [AllowNull]
    public string? Street { get; set; }
    [AllowNull]
    public string? Number { get; set; }
    [AllowNull]
    public string? City { get; set; }
    [AllowNull]
    public string? PostalCode { get; set; }
    [AllowNull]
    public string? Phone { get; set; }
    [AllowNull]
    public string? Email { get; set; }
    [AllowNull]
    public List<ApplicationUser> Users { get; } = new List<ApplicationUser> ();

}
