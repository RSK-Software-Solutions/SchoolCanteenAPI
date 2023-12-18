
using System.Reflection.Metadata;

namespace SchoolCanteen.DATA.Models;

public class Role
{
    public Guid RoleId { get; set; }
    public Guid CompanyId { get; set; }
    public string RoleName { get; set; }
    public List<User> Users { get; } = new();
    public List<UserRole> UserRoles { get; } = new();
}
