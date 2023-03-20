namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.UserAggregate
{
    public class AuthorizeModelDTO
    {
        public string AccessToken { get; set; }
        public UserDTO User { get; set; }
    }
}
