using InnoGotchiGameFrontEnd.BLL.AggregatesModel.PictureAggregate;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.UserAggregate
{
    public class AddUserDTOModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RePassword { get; set; }
        public PictureDTO? Picture { get; set; }
    }
}
