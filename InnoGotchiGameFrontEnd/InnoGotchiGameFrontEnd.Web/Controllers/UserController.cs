
using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.ComandModels.User;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model.Authorize;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.Web.ViewModels.PageSystem;
using InnoGotchiGameFrontEnd.Web.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InnoGotchiGameFrontEnd.Web.Controllers
{
    [Route("/")]
    public class UserController : BaseController
    {
        private int _pageSize;
        private UserManager _userManager;
        private AuthorizeModel _authorizeModel;
        public UserController(UserManager userManager, AuthorizeModel authorizeModel)
        {
            _userManager = userManager;
            _authorizeModel = authorizeModel;
            _pageSize = 5;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("users")]
        public async Task<IActionResult> Users(string sortRule, bool isDescendingSort, string lastNameTemplate, string emailTemplate, int pageNumber = 1)
        {
            sortRule = GetValueFromCookie(Request, "UserSortRule", nameof(sortRule), "");
            lastNameTemplate = GetValueFromCookie(Request, "UserLastName", nameof(lastNameTemplate), "");
            emailTemplate = GetValueFromCookie(Request, "UserEmail", nameof(emailTemplate), "");

            var sorter = GetSorter(sortRule);
            sorter.IsDescendingSort = isDescendingSort;

            var filtrator = new UserDTOFiltrator()
            {
                FirstName = lastNameTemplate,
                Email = emailTemplate
            };

            Response.Cookies.Append("UserSortRule", sortRule);
            Response.Cookies.Append("UserLastName", lastNameTemplate);
            Response.Cookies.Append("UserEmail", emailTemplate);

            var users = await _userManager.GetUsersPage(_pageSize, pageNumber, sorter, filtrator);

            var page = new Page(pageNumber, GetPageStatus(pageNumber, users.Count()));
            var viewModel = new AllUsersViewModel(users, page, sortRule, lastNameTemplate, emailTemplate, sorter.IsDescendingSort);
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
            if (token != null)
            {
                await SaveUserInCookie(token);
            }
            return Redirect("/");
        }

        [HttpGet("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.Response.Cookies.Delete("token");
            _userManager.CachClear();
            return RedirectToAction("LogIn");
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
                case "LastName":
                    sorter.SortRule = UserDTOSortRule.LastName;
                    break;
            }

            return sorter;
        }

        private PageStatus GetPageStatus(int pageNumber, int usersCount)
        {
            PageStatus pageStatus;
            if (pageNumber <= 1)
            {
                pageStatus = PageStatus.FirstPage;
            }
            else if (usersCount < _pageSize)
            {
                pageStatus = PageStatus.LastPage;
            }
            else
            {
                pageStatus = PageStatus.MiddlePage;
            }
            return pageStatus;
        }
    }
}
