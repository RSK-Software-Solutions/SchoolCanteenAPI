
using Microsoft.AspNetCore.Identity;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.DTOs.UserDTOs;

public class CreateUserDTO
{
    public Guid CompanyId { get; set; }
    public IdentityRole Role { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}
