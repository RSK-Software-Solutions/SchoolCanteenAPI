
namespace SchoolCanteen.Logic.Models;

public class User
{
    public Guid UserId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<Role> Roles { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserDetails UserDetails { get; set; }
}
