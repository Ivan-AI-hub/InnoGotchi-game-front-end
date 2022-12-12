namespace InnoGotchiGameFrontEnd.BLL.Model.Authorize
{
    public class AuthorizeModel
    {
        public string? AccessToken { get; set; }
        public UserDTO? User { get; set; }
        public bool IsAuthorized => AccessToken != null;
    }
}
