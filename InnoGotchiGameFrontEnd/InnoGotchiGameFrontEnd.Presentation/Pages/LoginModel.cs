using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.Presentation.Pages
{
    public class LoginModel : ComponentBase
    {
        public LoginModel()
        {
            LoginData = new LoginViewModel();
        }

        public LoginViewModel LoginData { get; set; }

        protected Task LoginAsync()
        {
            return Task.CompletedTask;
        }
    }

    public class LoginViewModel
    {
        [Required]
        [StringLength(14, ErrorMessage = "too long")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
