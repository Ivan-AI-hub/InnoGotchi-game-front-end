
using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.Web.ViewModels.PageSystem;

namespace InnoGotchiGameFrontEnd.Web.ViewModels.Users
{
    public class AllUsersViewModel
    {
        public AllUsersViewModel(IEnumerable<UserDTO> users, Page page, string sortRule, string lastNameTemplate, string emailTemplate, bool isDescendingSort)
        {
            Users = users;
            Page = page;
            SortRule = sortRule;
            LastNameTemplate = lastNameTemplate;
            EmailTemplate = emailTemplate;
            IsDescendingSort = isDescendingSort;
        }

        public IEnumerable<UserDTO> Users { get; set; }
        public Page Page { get; set; }
        public string SortRule { get; set; }
        public string LastNameTemplate { get; set; }
        public string EmailTemplate { get; set; }
        public bool IsDescendingSort { get; set; }
    }
}
