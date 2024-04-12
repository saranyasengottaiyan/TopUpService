using Microsoft.AspNetCore.Mvc;
using TopUpService.Models;


namespace TopUpService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET api/<UserController>/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            try
            {
                var result = await userService.GetUserByIdAsync(userId);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // POST api/<UserController>
        [HttpPost("user")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage));

                    if (message.Contains("Security issue found"))
                        return BadRequest($"{"Security issue found. Please revise content."}");

                    return BadRequest($"{"Invalid input"}");
                }
                var result = await userService.CreateUserAsync(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("user/{id}")]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest($"{"Invalid input"}");

                var result = await userService.EditUserAsync(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            try
            {
                var result = await userService.DeleteUserAsync(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
