using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using UserTaskApi.DTO;

namespace UserTaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return await userService.GetUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await userService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult> PostUser(UserDTO userDto)
        {
            await userService.AddUserAsync(userDto);

            return Ok();
        }
    }
}
