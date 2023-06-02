using Microsoft.AspNetCore.Mvc;

namespace Prediction.Model
{
    public class LoginModel
    {
        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }
    }
}
