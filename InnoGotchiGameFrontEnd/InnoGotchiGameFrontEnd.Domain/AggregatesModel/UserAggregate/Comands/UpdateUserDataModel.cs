using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Comands
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

        public UpdateUserDataModel(int updatedId, string firstName, string lastName, Picture? picture)
        {
            UpdatedId = updatedId;
            FirstName = firstName;
            LastName = lastName;
            Picture = picture;
        }
    }
}
