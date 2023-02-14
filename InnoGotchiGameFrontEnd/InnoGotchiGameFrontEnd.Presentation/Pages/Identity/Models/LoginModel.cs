using AuthorizationInfrastructure;
using AuthorizationInfrastructure.Tokens;
using InnoGotchiGameFrontEnd.BLL;
using Microsoft.AspNetCore.Components;

namespace InnoGotchiGameFrontEnd.Presentation.Pages.Identity.Models
{
    public class LoginModel : ComponentBase
    {
        [Inject] public IStorageService LocalStorageService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public UserManager Manager { get; set; }

        protected LoginData LoginData { get; set; }
        protected bool IsLoading { get; set; }
        protected string ErrorMessage { get; set; }

        public LoginModel()
        {
            LoginData = new LoginData();
            ErrorMessage = "";
        }

        protected async Task LoginAsync()
        {
            IsLoading = true;
            var authModel = await Manager.Authorize(LoginData.Email, LoginData.Password);
            if (authModel != null)
            {
                var token = new SecurityToken
                {
                    AccessToken = authModel.AccessToken,
                    UserId = authModel.User.Id,
                    Email = authModel.User.Email,
                    FarmId = authModel.User.OwnPetFarmId,
                    UserName = $"{authModel.User.FirstName} {authModel.User.LastName}",
                    ExpireAt = DateTime.UtcNow.AddHours(1)
                };
                await LocalStorageService.SetAsync(nameof(SecurityToken), token);
                Navigation.NavigateTo("/", true);
            }
            else
            {
                ErrorMessage = "Email или пароль введены неверно.";
            }
            IsLoading = false;
        }
    }
}
