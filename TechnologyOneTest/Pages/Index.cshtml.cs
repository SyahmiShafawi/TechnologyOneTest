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

        [BindProperty]
        public string NumberDB { get; set; }
        //public string ResultDB { get; private set; }

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> OnPostConvertNumberLocalAsync()
        {
            if (string.IsNullOrWhiteSpace(Number))
            {
                ModelState.AddModelError("", "Please enter a valid number.");
                return Page();
            }

            try
            {
                Result = await _apiService.ConvertNumberToWordsLocalAsync(Number);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostConvertNumberDBAsync()
        {
            if (string.IsNullOrWhiteSpace(NumberDB))
            {
                ModelState.AddModelError("", "Please enter a valid number.");
                return Page();
            }

            try
            {
                Result = await _apiService.ConvertNumberToWordsDBAsync(NumberDB);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
            }

            return Page();
        }

    }
}
