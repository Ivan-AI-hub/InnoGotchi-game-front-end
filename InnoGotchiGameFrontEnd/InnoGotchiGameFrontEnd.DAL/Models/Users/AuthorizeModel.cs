namespace InnoGotchiGameFrontEnd.DAL.Models.Users
{
	public class AuthorizeModel
	{
		public string AccessToken { get; set; }
        public User user { get; set; }
	}
}
