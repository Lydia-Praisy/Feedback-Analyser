using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Prediction.Model;

namespace Prediction.Pages
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public StudentPerformanceViewModel StudentPerformanceViewModel { get; set; }

        public IActionResult OnGet()
        {
            return this.Page();
        }

        public async Task<IActionResult> OnPostStudentPerformance(StudentPerformanceViewModel model)
        {
            var trainer = new Trainer();
            DashboardViewModel source = await trainer.GetDashboardSourceAsync(model);
            TempData["DashboardSource"] = JsonConvert.SerializeObject(source);
            
            return this.RedirectToPage("/Dashboard");
        }
    }
}
