
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SchoolCanteen.DATA.Models;

public class Company
{
    public Guid CompanyId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set;}
    public int Nip { get; set;}
    [AllowNull]
    [MaxLength(100)]
    public string? Street { get; set; }
    [AllowNull]
    [MaxLength(25)]
    public string? Number { get; set; }
    [AllowNull]
    [MaxLength(100)]
    public string? City { get; set; }
    [AllowNull]
    [MaxLength(10)]
    public string? PostalCode { get; set; }
    [AllowNull]
    [MaxLength(25)]
    public string? Phone { get; set; }
    [AllowNull]
    [MaxLength(100)]
    public string? Email { get; set; }
    [AllowNull]
    public List<ApplicationUser> Users { get; } = new List<ApplicationUser> ();
    public List<Recipe> Recipes { get; } = new List<Recipe> ();
    public List<Product> Products { get; } = new List<Product>();
    public List<ProductStorage> ProductsStorage { get; } = new List<ProductStorage>();
    public List<FinishedProduct> FinishedProducts { get; } = new List<FinishedProduct> ();
    public List<Unit> Units { get; } = new List<Unit>();

}
