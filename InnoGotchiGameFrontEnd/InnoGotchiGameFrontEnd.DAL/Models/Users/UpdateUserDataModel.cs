using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.DAL.Models.Users
{
    public class UpdateUserDataModel
    {
        [Required]
        public int UpdatedId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public Picture? Picture { get; set; }
    }
}
