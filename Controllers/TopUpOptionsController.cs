using Microsoft.AspNetCore.Mvc;
using TopUpService.Interfaces;


namespace TopUpService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopUpOptionsController : ControllerBase
    {
        private readonly IOptionsService optionsService;
        public TopUpOptionsController(IOptionsService optionsService)
        {
            this.optionsService = optionsService;
        }
        // GET: api/<TopUpController>
        [HttpGet("options")]
        public async Task<IActionResult> GetAllOptions()
        {
            try
            {
                var result = await optionsService.GetAllTopupOptionsAsync();
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // GET api/<TopUpController>/5
        [HttpGet("options/{id}")]
        public async Task<IActionResult> Get(Guid optionsId)
        {
            try
            {
                var result = await optionsService.GetTopUpOptionByIdAsync(optionsId);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // POST api/<TopUpController>
        [HttpPost("options")]
        public async Task<IActionResult> Post([FromBody] TopUpOption option)
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
                var result = await optionsService.CreateTopUpOptionAsync(option);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // PUT api/<TopUpController>/5
        [HttpPut("options/{id}")]
        public async Task<IActionResult> Put([FromBody] TopUpOption option)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest($"{"Invalid input"}");

                var result = await optionsService.EditTopUpOptionAsync(option);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // DELETE api/<TopUpController>/5
        [HttpDelete("options/{id}")]
        public async Task<IActionResult> Delete(Guid optionId)
        {
            try
            {
                var result = await optionsService.DeleteTopUpOptionAsync(optionId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}