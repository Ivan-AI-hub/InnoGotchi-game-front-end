
using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.ComandModels.User;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model.Authorize;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.Web.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.Web.Controllers
{
    [Route("/")]
	public class UserController : BaseController
	{
		private UserManager _userManager;
        private AuthorizeModel _authorizeModel;
        public UserController(UserManager userManager, AuthorizeModel authorizeModel)
        {
			_userManager = userManager;
            _authorizeModel = authorizeModel;
		}

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("users")]
		public async Task<IActionResult> Users(string sortRule, bool isDescendingSort)
		{
            sortRule = GetValueFromCookie(Request, "UserSortRule", nameof(sortRule), "");
            var sorter = GetSorter(sortRule);
            sorter.IsDescendingSort = isDescendingSort;

            var filtrator = new UserDTOFiltrator();
            Response.Cookies.Append("UserSortRule", sortRule);

			var users = await _userManager.GetAllUsers(sorter, filtrator);
            var viewModel = new AllUsersViewModel(users, sortRule, sorter.IsDescendingSort);
			return View(viewModel);
		}

		[HttpGet]
		[Route("{userId}")]
        public async Task<IActionResult> UserPage(int userId)
        {
            var user = await _userManager.GetUserById(userId);
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
			var token = await _userManager.Authorize(email, password);
			if(token != null)
			{
                await SaveUserInCookie(token);                
			}
			return Redirect("/");
        }

        [HttpGet("UpdateData")]
        public IActionResult UpdateData()
        {
            return View();
        }

        [HttpPost("UpdateData")]
        public async Task<IActionResult> UpdateData(UpdateUserDTODataModel updateModel)
        {
            updateModel.UpdatedId = _authorizeModel.User.Id;
            var isComplite = await _userManager.UpdateUserData(updateModel);

            return Redirect("/");
        }

        [HttpGet("UpdatePassword")]
        public IActionResult UpdatePassword()
        {
            return View("UpdatePassword", "");
        }

        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdateUserDTOPasswordModel updateModel)
        {
            updateModel.UpdatedId = _authorizeModel.User.Id;
            var rezult = await _userManager.UpdateUserPassword(updateModel);
            if (rezult.IsComplete)
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
        public async Task<IActionResult> Register(AddUserDTOModel addModel)
        {
            var rezult = await _userManager.Create(addModel);
            if (rezult.IsComplete)
            {
                var token = await _userManager.Authorize(addModel.Email, addModel.Password);
                await SaveUserInCookie(token);
            }
            return Redirect("/");
        }

        [HttpGet("LogOut")]
        public IActionResult LogOut()
		{
            HttpContext.Session.Remove("token");
            
            return RedirectToAction("LogIn");
        }

        private async Task SaveUserInCookie(string token)
        {
            var authorizeModel = new AuthorizeModel() { AccessToken = token };
            HttpContext.Response.Cookies.Append("token", JsonSerializer.Serialize(authorizeModel));
        }

        private UserDTOSorter GetSorter(string sortRule)
        {
            var sorter = new UserDTOSorter();
            switch (sortRule)
            {
                case "Email":
                    sorter.SortRule = UserDTOSortRule.Email;
                    break;
                case "FirstName":
                    sorter.SortRule = UserDTOSortRule.FirstName;
                    break;
            }

            return sorter;
        }
    }
}
