using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Comands
{
    public class UpdateUserPasswordModel
    {
        [Required]
        public int UpdatedId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }

        public UpdateUserPasswordModel(int updatedId, string oldPassword, string newPassword)
        {
            UpdatedId = updatedId;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }
}
