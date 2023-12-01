
using System.Reflection.Metadata;

namespace SchoolCanteen.Logic.Models;

public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public List<User> Users { get; set; }
}
