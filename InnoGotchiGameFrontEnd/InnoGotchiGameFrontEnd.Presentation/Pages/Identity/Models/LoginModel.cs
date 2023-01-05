using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.ComandModels.User;
using InnoGotchiGameFrontEnd.BLL.Model.Identity;
using InnoGotchiGameFrontEnd.Presentation.Infrastructure;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.Presentation.Pages.Identity.Models
{
    public class LoginModel : ComponentBase
    {
        [Inject] public ILocalStorageService LocalStorageService { get; set; }
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
                    HasFarm = authModel.User.OwnPetFarm != null,
                    UserName = $"{authModel.User.FirstName} {authModel.User.LastName}",
                    EmailsCount = authModel.User.UnconfirmedRequests.Count() + authModel.User.RejectedRequests.Count(),
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
