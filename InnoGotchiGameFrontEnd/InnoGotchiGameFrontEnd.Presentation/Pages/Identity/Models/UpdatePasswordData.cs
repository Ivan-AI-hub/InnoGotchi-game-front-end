using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.Presentation.Pages.Identity.Models
{
    public class UpdatePasswordData
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
