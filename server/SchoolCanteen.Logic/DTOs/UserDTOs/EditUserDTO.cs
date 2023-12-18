
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.DTOs.UserDTOs;

public class EditUserDTO
{
    public Guid UserId { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Role> Roles { get; set; }
    public UserDetails UserDetails { get; set; }    
}
