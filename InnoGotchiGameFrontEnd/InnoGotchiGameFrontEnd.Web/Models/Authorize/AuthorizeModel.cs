using InnoGotchiGameFrontEnd.Web.Models.Users;

namespace InnoGotchiGameFrontEnd.Web.Models.Authorize
{
    public class AuthorizeModel
    {
        public string? AccessToken { get; set; }
        public User User { get; set; }
        public bool IsAuthorized => AccessToken != null;
    }
}
