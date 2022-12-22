namespace InnoGotchiGameFrontEnd.BLL.Model.Identity
{
	public class AuthorizeModelDTO
	{
		public string AccessToken { get; set; }
		public UserDTO user { get; set; }
	}
}
