using InnoGotchiGameFrontEnd.Presentation.Infrastructure;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.Presentation.Pages.Identity.Models
{
    public class LoginModel : ComponentBase
    {
        [Inject] public ILocalStorageService LocalStorageService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        public LoginModel()
        {
            LoginData = new LoginViewModel();
        }

        public LoginViewModel LoginData { get; set; }

        protected async Task LoginAsync()
        {
            var token = new SecurityToken
            {
                AccessToken = "21421412",
                UserName = LoginData.UserName,
                ExpireAt = DateTime.UtcNow.AddHours(1)
            };
            await LocalStorageService.SetAsync(nameof(SecurityToken), token);
            Navigation.NavigateTo("/", true);
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
