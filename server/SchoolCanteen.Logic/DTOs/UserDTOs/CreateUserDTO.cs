
namespace SchoolCanteen.Logic.DTOs.UserDTOs;

public class CreateUserDTO
{
    public Guid UserId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid UserDetailsId { get; set; }
    public Guid RoleId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}
