using InnoGotchiGame.Web.Models.Users;
using InnoGotchiGameFrontEnd.Web.Models.Authorize;
using InnoGotchiGameFrontEnd.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.Web.Controllers
{
	public class UserController : BaseController
	{
		private UserService _userService;
        public UserController(UserService userService)
        {
			_userService = userService;
		}

        [HttpGet]
		public async Task<IActionResult> Index()
		{
			var users = await _userService.OnGet();
			return View(users);
		}

		[HttpGet]
		[Route("users/{userId}")]
        public async Task<IActionResult> UserPage(int userId)
        {
            var user = await _userService.OnGet(userId);
            return View(user);
        }

        [HttpGet]
		public IActionResult LogIn()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password)
        {
			var token = await _userService.Authorize(email, password);
			if(token != null)
			{
                await SaveUserInSession(token);                
			}
			return Redirect("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AddUserModel addModel)
        {
            var complite = await _userService.OnPost(addModel);
            if (complite)
            {
                var token = await _userService.Authorize(addModel.Email, addModel.Password);
                await SaveUserInSession(token);
            }
            return Redirect("/");
        }
        [HttpGet]
        public IActionResult LogOut()
		{
            HttpContext.Session.Remove("token");
            
            return RedirectToAction("LogIn");
        }

        private async Task SaveUserInSession(string token)
        {
            var authorizeModel = new AuthorizeModel() { AccessToken = token, User = await _userService.OnGetAuthodizedUser() };
            HttpContext.Session.SetString("token", JsonSerializer.Serialize(authorizeModel));
        }
    }
}
