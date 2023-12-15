
namespace SchoolCanteen.DATA.Models;

public class User
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid UserDetailsId { get; set; }
    public Guid RoleId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public Company Company { get; set; } = null!;
    public UserDetails? UserDetails { get; set; } = null!;
    public List<UserRole> UserRoles { get; } = new();
    public List<Role> Roles { get; } = new();
}
