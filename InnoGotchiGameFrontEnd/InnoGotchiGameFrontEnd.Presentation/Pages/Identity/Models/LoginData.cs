using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.Presentation.Pages.Identity.Models
{
    public class LoginData
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
