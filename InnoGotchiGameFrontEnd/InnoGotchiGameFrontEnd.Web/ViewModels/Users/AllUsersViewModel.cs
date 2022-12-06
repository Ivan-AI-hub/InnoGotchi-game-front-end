using InnoGotchiGameFrontEnd.Web.Models.Users;

namespace InnoGotchiGameFrontEnd.Web.ViewModels.Users
{
    public class AllUsersViewModel
    {
        public AllUsersViewModel(IEnumerable<User> users, string sortRule, bool isDescendingSort)
        {
            Users = users;
            SortRule = sortRule;
            IsDescendingSort = isDescendingSort;
        }

        public IEnumerable<User> Users { get; set; }
       public string SortRule { get; set; }
       public bool IsDescendingSort { get; set; }
    }
}
