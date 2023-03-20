using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Comands
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
        public AddUserModel(string firstName, string lastName, string email, string password, Picture? picture)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Picture = picture;
        }
    }
}
