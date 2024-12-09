using Microsoft.AspNetCore.Mvc;

namespace TechnologyOneTest.Helper
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApiHandlerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiHandlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("ConvertNumberToWordsLocal")]
        public IActionResult ConvertNumberToWords([FromForm] NumberInput input)
        {
            if (decimal.TryParse(input.Number, out var decimalNumber))
            {
                var words = NumberToWordsConverter.ConvertToWordsLocal(decimalNumber);
                return Ok (new { Input = input.Number, Words = words });
            }
            return BadRequest("Invalid input. Please provide a valid number.");
        }

        [HttpPost("ConvertNumberToWordsDB")]
        public IActionResult ConvertNumberToWordsDB([FromForm] NumberInput input)
        {
            if (decimal.TryParse(input.Number, out var decimalNumber))
            {
                var converter = new NumberToWordsConverter(_context); // Pass the DbContext
                var words = converter.ConvertToWordsDB(decimalNumber); // Call the instance method
                return Ok(new { Input = input.Number, Words = words });
            }
            return BadRequest("Invalid input. Please provide a valid number.");
        }
    }

    public class NumberInput
    {
        public string Number { get; set; }
    }

}

