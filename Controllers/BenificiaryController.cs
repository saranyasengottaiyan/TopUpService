using Microsoft.AspNetCore.Mvc;
using TopUpService.Interfaces;

namespace TopUpService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenificiaryController : ControllerBase
    {
        private readonly IBenificiariesService benificiariesService;
        public BenificiaryController(IBenificiariesService service)
        {
            this.benificiariesService = service;
        }
        // GET: api/<BenificiaryController>
        [HttpGet("benificiaries/{userId}")]
        public async Task<IActionResult> GetAllBenificiaries(Guid userId)
        {
            try
            {
                var result = await benificiariesService.GetAllBeneficiariesAsync(userId);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // GET api/<BenificiaryController>/5
        [HttpGet("benificiaries/{id}")]
        public async Task<IActionResult> Get(Guid beneficiaryId)
        {
            try
            {
                var result = await benificiariesService.GetBeneficiaryByIdAsync(beneficiaryId);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // POST api/<BenificiaryController>
        [HttpPost("benificiaries")]
        public async Task<IActionResult> Post([FromBody] Beneficiary beneficiary)
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
                var result = await benificiariesService.CreateBeneficiaryAsync(beneficiary);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // PUT api/<BenificiaryController>/5
        [HttpPut("benificiaries/{id}")]
        public async Task<IActionResult> Put([FromBody] Beneficiary beneficiary)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest($"{"Invalid input"}");

                var result = await benificiariesService.EditBeneficiaryAsync(beneficiary);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // DELETE api/<BenificiaryController>/5
        [HttpDelete("benificiaries/{id}")]
        public async Task<IActionResult> Delete(Guid beneficiaryId)
        {
            try
            {
                var result = await benificiariesService.DeleteBeneficiaryAsync(beneficiaryId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
