using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.DAL.Models.Users;
using InnoGotchiGameFrontEnd.Presentation.Components.PageSystem;

namespace InnoGotchiGameFrontEnd.Presentation.Pages.Users.Models
{
    public class UsersData
    {
        public IEnumerable<UserDTO> Users { get; set; }
        public UserFiltrator Filtrator { get; set; }
        public UserSorter Sorter { get; set; }
        public Page Page { get; set; }
        public UsersData()
        {
            Users = new List<UserDTO>();
            Filtrator = new UserFiltrator();
            Sorter = new UserSorter();
            Page = new Page(1, 10);
        }
    }
}
