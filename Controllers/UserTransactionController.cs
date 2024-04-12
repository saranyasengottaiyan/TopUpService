using Microsoft.AspNetCore.Mvc;
using TopUpService.Models;


namespace TopUpService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        public UserTransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }
        // GET: api/<UserTransactionController>
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserTransactionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserTransactionController>
        [HttpPost("topup")]
        public async Task<IActionResult> TopUpTransaction([FromBody] UserTransaction transaction)
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
                var result = await transactionService.TopUpTransaction(transaction);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // PUT api/<UserTransactionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserTransactionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
