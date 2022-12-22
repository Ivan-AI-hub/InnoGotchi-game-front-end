namespace InnoGotchiGameFrontEnd.Presentation
{
    public class SecurityToken
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
