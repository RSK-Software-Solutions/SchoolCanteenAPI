
namespace SchoolCanteen.DATA.Models;

public class UserRole
{
    public Guid UserForeignId { get; set; }
    public Guid RoleForeignId { get; set; }
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}
