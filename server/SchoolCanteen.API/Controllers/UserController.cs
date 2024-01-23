using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services.User;

namespace SchoolCanteen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }


        [HttpGet("/api/users"), Authorize (Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SimpleUserDTO>>> GetAllAsync()
        {
            try
            {
                var users = await userService.GetAllAsync();
                if (users.Count() == 0) return NotFound($"No users found.");

                return Ok(users);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/users:loginName"), Authorize (Roles = "Admin")]
        public async Task<ActionResult<SimpleUserDTO>> GetByNameAsync([FromQuery] string UserName)
        {
            try
            {
                var user = await userService.GetByNameAsync(UserName);
                if (user == null) return NotFound($"Not found {UserName}.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest($"Could not find {UserName}");
            }
        }

        [HttpPost("/api/users"), Authorize(Roles ="Admin")]
        public async Task<ActionResult<SimpleUserDTO>> CreateAsync([FromBody] CreateUserDTO newUser)
        {
            try
            {
                var user = await userService.CreateAsync(newUser);

                return Ok( user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest($"Could not create new user - {newUser.Email}");
            }
        }

        [HttpPut("/api/users"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> EditAsync([FromBody] EditUserDTO editUser)
        {
            try
            {
                return Ok(await userService.UpdateAsync(editUser));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/api/users"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteAsync([FromQuery] Guid id)
        {
            try
            {
                return Ok(await userService.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
