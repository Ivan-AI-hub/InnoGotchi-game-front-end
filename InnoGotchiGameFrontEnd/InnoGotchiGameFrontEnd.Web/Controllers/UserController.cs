using InnoGotchiGame.Web.Models.Users;
using InnoGotchiGameFrontEnd.Web.Models.Authorize;
using InnoGotchiGameFrontEnd.Web.Models.Users;
using InnoGotchiGameFrontEnd.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.Web.Controllers
{
    [Route("/")]
	public class UserController : BaseController
	{
		private UserService _userService;
        private AuthorizeModel _authorizeModel;
        public UserController(UserService userService, AuthorizeModel authorizeModel)
        {
			_userService = userService;
            _authorizeModel = authorizeModel;
		}

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("users")]
		public async Task<IActionResult> Users()
		{
			var users = await _userService.OnGet();
			return View(users);
		}

		[HttpGet]
		[Route("{userId}")]
        public async Task<IActionResult> UserPage(int userId)
        {
            var user = await _userService.OnGet(userId);
            return View(user);
        }

        [HttpGet("LogIn")]
		public IActionResult LogIn()
		{
			return View();
		}

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(string email, string password)
        {
			var token = await _userService.Authorize(email, password);
			if(token != null)
			{
                await SaveUserInSession(token);                
			}
			return Redirect("/");
        }

        [HttpGet("UpdateData")]
        public IActionResult UpdateData()
        {
            return View();
        }

        [HttpPost("UpdateData")]
        public async Task<IActionResult> UpdateData(UpdateUserDataModel updateModel)
        {
            updateModel.UpdatedId = _authorizeModel.User.Id;
            var isComplite = await _userService.OnUpdateData(updateModel);

            return Redirect("/");
        }

        [HttpGet("UpdatePassword")]
        public IActionResult UpdatePassword()
        {
            return View("UpdatePassword", "");
        }

        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdateUserPasswordModel updateModel)
        {
            updateModel.UpdatedId = _authorizeModel.User.Id;
            var isComplite = await _userService.OnUpdatePassword(updateModel);
            if (isComplite)
            {
                return Redirect("/");
            }
            else
            {
                return View("UpdatePassword", "Старый пароль неверен");
            }
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
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

        [HttpGet("LogOut")]
        public IActionResult LogOut()
		{
            HttpContext.Session.Remove("token");
            
            return RedirectToAction("LogIn");
        }

        private async Task SaveUserInSession(string token)
        {
            var authorizeModel = new AuthorizeModel() { AccessToken = token };
            HttpContext.Session.SetString("token", JsonSerializer.Serialize(authorizeModel));
        }
    }
}
