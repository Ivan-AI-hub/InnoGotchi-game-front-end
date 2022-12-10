using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.DAL.Models.Users
{
    public class UpdateUserPasswordModel
    {
        [Required]
        public int UpdatedId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
