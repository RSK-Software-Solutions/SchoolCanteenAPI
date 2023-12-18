
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.DTOs.UserDTOs;

public class CreateUserDTO
{
    public Guid CompanyId { get; set; }
    public Role Role { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}
