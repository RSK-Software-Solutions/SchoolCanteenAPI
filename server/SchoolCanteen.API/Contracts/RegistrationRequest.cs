using System.ComponentModel.DataAnnotations;

namespace SchoolCanteen.API.Contracts;

public record RegistrationRequest(
    [Required] string Email,
    [Required] string UserName,
    [Required] string Password,
    [Required] string Role
    );

