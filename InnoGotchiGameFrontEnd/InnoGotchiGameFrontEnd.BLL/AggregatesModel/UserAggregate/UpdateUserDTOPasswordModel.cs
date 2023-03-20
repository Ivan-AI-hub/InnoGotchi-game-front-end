using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.UserAggregate
{
    public class UpdateUserDTOPasswordModel
    {
        [Required]
        public int UpdatedId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
