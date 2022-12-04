using InnoGotchiGameFrontEnd.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchiGameFrontEnd.Web.Controllers
{
	public class UserController : Controller
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
			await _userService.Authorize(email, password);
			return Redirect("/");
        }
    }
}
