
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.CompanyDTOs;
using SchoolCanteen.Logic.DTOs.RoleDTOs;

namespace SchoolCanteen.Logic.DTOs.UserDTOs;

public class SimpleUserDTO
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Roles { get; set; } = new List<string>();

}
