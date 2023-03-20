using InnoGotchiGameFrontEnd.BLL.AggregatesModel.UserAggregate;
using InnoGotchiGameFrontEnd.Presentation.Components.PageSystem;

namespace InnoGotchiGameFrontEnd.Presentation.Pages.Users.Models
{
    public class UsersData
    {
        public int AuthorizedUserId { get; set; }
        public IEnumerable<UserDTO>? Users { get; set; }
        public UserDTOFiltrator Filtrator { get; set; }
        public UserDTOSorter Sorter { get; set; }
        public Page Page { get; set; }
        public PageStatus PageStatus => Page.GetPageStatus(Users == null ? 0 : Users.Count());
        public UsersData()
        {
            Users = null;
            Filtrator = new UserDTOFiltrator();
            Sorter = new UserDTOSorter();
            Page = new Page(1, 5);
        }
    }
}
