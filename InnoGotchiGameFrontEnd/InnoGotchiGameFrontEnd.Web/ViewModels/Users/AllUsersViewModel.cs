
using InnoGotchiGameFrontEnd.BLL.Model;

namespace InnoGotchiGameFrontEnd.Web.ViewModels.Users
{
    public class AllUsersViewModel
    {
        public AllUsersViewModel(IEnumerable<UserDTO> users, string sortRule, bool isDescendingSort)
        {
            Users = users;
            SortRule = sortRule;
            IsDescendingSort = isDescendingSort;
        }

        public IEnumerable<UserDTO> Users { get; set; }
       public string SortRule { get; set; }
       public bool IsDescendingSort { get; set; }
    }
}
