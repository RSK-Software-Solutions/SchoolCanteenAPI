namespace SchoolCanteen.Logic.DTOs.RoleDTOs;

public class SimpleRoleDTO
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public SimpleRoleDTO(Guid roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }
}
