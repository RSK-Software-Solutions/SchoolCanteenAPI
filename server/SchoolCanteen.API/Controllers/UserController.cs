using Microsoft.AspNetCore.Mvc;
using SchoolCanteen.DATA.Models;
using SchoolCanteen.Logic.DTOs.UserDTOs;
using SchoolCanteen.Logic.Services.Interfaces;

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


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<SimpleUserDTO>>> GetAllAsync([FromQuery] Guid companyId)
        {
            try
            {
                var users = await userService.GetAllAsync(companyId);
                if (users.Count() == 0) return NotFound($"No users found.");

                return Ok(users);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByName")]
        public async Task<ActionResult<User>> GetByNameAsync([FromQuery] string LoginName, Guid companyId)
        {
            try
            {
                var user = await userService.GetByNameAsync(LoginName, companyId);
                if (user == null) return NotFound($"Not found {LoginName}.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest($"Could not find {LoginName}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateAsync([FromBody] CreateUserDTO newUser)
        {
            try
            {
                var user = await userService.CreateAsync(newUser);

                return Ok( user);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return BadRequest($"Could not create new user - {newUser.Login}");
            }
        }

        [HttpPut]
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

        [HttpDelete]
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
