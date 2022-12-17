using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.DAL.Models.Users
{
    public class AddUserModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Picture? Picture { get; set; }
    }
}
