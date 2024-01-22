
using Microsoft.AspNetCore.Identity;
using SchoolCanteen.DATA.Models;

namespace SchoolCanteen.Logic.DTOs.UserDTOs;

public class CreateUserDTO
{
    public Guid CompanyId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RoleName { get; set; }
}
