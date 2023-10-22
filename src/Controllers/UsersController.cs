using Microsoft.AspNetCore.Mvc;
using src.Models;
using src.Services;

namespace src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] User user)
        {
            await _userService.CreateUser(user);
            return Created($"User {user.Id} created", user);
        }


        [HttpPut]
        [ActionName(nameof(UpdateAsync))]
        public async Task<IActionResult> UpdateAsync([FromBody] User user)
        {
            if(user.Id == null)
                return BadRequest("Id can not be null");

            await _userService.ReplaceAsync(user.Id, user);
            return Created($"User {user.Id} updated", user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
             User record = await _userService.GetAsync(id);

            if(record == null)
                return NotFound("User not found");

            await _userService.DeleteAsync(id);
            return Ok("User deleted");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            User record = await _userService.GetAsync(id);

            if(record == null)
                return NotFound("User not found");

            return Ok(record);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<User> records = await _userService.GetAsync();

            if(!records.Any())
                return NotFound("No users found");

            return Ok(records);
        }

    }
}
