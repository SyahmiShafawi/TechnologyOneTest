using Microsoft.AspNetCore.Mvc;

namespace TechnologyOneTest.Helper
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApiHandlerController : ControllerBase
    {
        [HttpPost("ConvertNumberToWords")]
        public IActionResult ConvertNumberToWords([FromForm] NumberInput input)
        {
            if (decimal.TryParse(input.Number, out var decimalNumber))
            {
                var words = NumberToWordsConverter.ConvertToWords(decimalNumber);
                return Ok (new { Input = input.Number, Words = words });
            }
            return BadRequest("Invalid input. Please provide a valid number.");
        }
    }

    public class NumberInput
    {
        public string Number { get; set; }
    }

}

