namespace SchoolCanteen.Logic.DTOs.RoleDTOs;

public class SimpleRoleDTO
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }
    public SimpleRoleDTO(Guid Id, string roleName)
    {
        this.Id = Id;
        RoleName = roleName;
    }
}
