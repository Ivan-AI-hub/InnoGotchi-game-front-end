namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate
{
    public class AuthorizeModel
    {
        public string AccessToken { get; set; }
        public User User { get; set; }
    }
}
