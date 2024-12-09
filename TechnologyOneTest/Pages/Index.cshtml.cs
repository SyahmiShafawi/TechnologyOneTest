using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechnologyOneTest.Helper;

namespace TechnologyOneTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;

        [BindProperty]
        public string Number { get; set; }

        public string Result { get; private set; }

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> OnPostConvertNumberAsync()
        {
            if (string.IsNullOrWhiteSpace(Number))
            {
                ModelState.AddModelError("", "Please enter a valid number.");
                return Page();
            }

            try
            {
                Result = await _apiService.ConvertNumberToWordsAsync(Number);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
            }

            return Page();
        }

        //private readonly ILogger<IndexModel> _logger;
        //private readonly IConfiguration _configuration;

        //public string BaseUrl { get; private set; }


        //public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        //{
        //    _logger = logger;
        //    _configuration = configuration;

        //}


        //public void OnGet()
        //{
        //    BaseUrl = _configuration["AppSettings:BaseUrl"];
        //}

    }
}
