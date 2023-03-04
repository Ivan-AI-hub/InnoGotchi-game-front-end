using AuthorizationInfrastructure;
using AuthorizationInfrastructure.Tokens;
using InnoGotchiGameFrontEnd.BLL.AggregatesModel.UserAggregate;
using InnoGotchiGameFrontEnd.Presentation.Components;
using Microsoft.AspNetCore.Components;

namespace InnoGotchiGameFrontEnd.Presentation.Pages.Identity.Models
{
    public class LoginModel : CancellableComponent
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
            var authModel = await Manager.Authorize(LoginData.Email, LoginData.Password, _cts.Token);
            if (authModel == null)
            {
                ErrorMessage = "Email или пароль введены неверно.";
                IsLoading = false;
                return;
            }

            var token = new SecurityToken(authModel.AccessToken,authModel.User.Id,$"{authModel.User.FirstName} {authModel.User.LastName}",
                                          authModel.User.Email, authModel.User.OwnPetFarmId,DateTime.UtcNow.AddHours(1));

            await LocalStorageService.SetAsync(nameof(SecurityToken), token);
            Navigation.NavigateTo("/", true);
            
            IsLoading = false;
        }
    }
}
